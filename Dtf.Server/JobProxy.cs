using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using Dtf.Core;

namespace Dtf.Server
{
    public class JobProxy : IJobProxy
    {
        static readonly string Error_HavePendingRequestAlready = "Have pending request already!";
        private static Dictionary<string, Job> _clientsJob = new Dictionary<string, Job>();
        //private const int WaitTime = 30 * 1000;
        private static readonly TimeSpan ExecuteTimeout;
        private static readonly TimeSpan LongLoopTimeout;

        private static readonly TimeSpan CallerConnectCheckTime = TimeSpan.FromMilliseconds(5000);
        private static readonly TimeSpan MonitorTimeout = TimeSpan.FromMilliseconds(500);

        static JobProxy()
        {
            string longLoopTimeoutStr = System.Configuration.ConfigurationManager.AppSettings["LongLoopTimeout"];
            string executeTimeoutStr = System.Configuration.ConfigurationManager.AppSettings["ExecuteTimeout"];
            Log.Default.Trace("longLoopTimeout:{0}s Execute timeout:{1}s", longLoopTimeoutStr, executeTimeoutStr);
            ExecuteTimeout = TimeSpan.FromSeconds(int.Parse(executeTimeoutStr));
            LongLoopTimeout = TimeSpan.FromSeconds(int.Parse(longLoopTimeoutStr));
        }

        public void ExecuteJob(string client)
        {
            Job job = GetOrCreateJob(client);
            using (AutoMonitor m = new AutoMonitor(job.InvokeLock, MonitorTimeout))
            {
                if (m.Entered)
                {
                    if (job.Status == JobStatus.Idle)
                    {
                        try
                        {
                            job.Message = HttpCurrentContext.Body;
                            job.Status = JobStatus.Scheduled;
                            //Log.Default.Trace(" Client:{0} Job:{1}", client, job.Message);
                            if (Wait(job, JobStatus.Finished, ExecuteTimeout))
                            {
                                string msg = job.Message;
                                msg = msg.Length > 1000 ? msg.Substring(0, 1000) : msg;
                                //Log.Default.Trace(" Client:{0} Result:{1}", client, msg);
                                HttpCurrentContext.Close(HttpStatusCode.OK, job.Message);
                            }
                            else
                            {
                                HttpCurrentContext.CloseAsTimeout();
                            }                            
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            job.Status = JobStatus.Idle;
                            Log.Default.Trace("Client:{0} Idle", client);
                        }
                    }
                    else
                    {
                        HttpCurrentContext.Close(HttpStatusCode.Forbidden, "Job status not Idle!");
                    }
                }
                else
                {
                    HttpCurrentContext.Close(HttpStatusCode.Forbidden, Error_HavePendingRequestAlready);
                }               
            }            
        }

        public void GetJobWait(string client)
        {
            Job job = GetOrCreateJob(client);
            using (AutoMonitor m = new AutoMonitor(job.ExecuteLock, MonitorTimeout))
            {
                if (m.Entered)
                {
                    DateTime start = DateTime.Now;
                    if (Wait(job, JobStatus.Scheduled, LongLoopTimeout))
                    {
                        //Log.Default.Trace(" Client:{0} job:{1}", client, job.Message);
                        try
                        {
                            HttpCurrentContext.Close(HttpStatusCode.OK, job.Message);
                            job.Status = JobStatus.Running;
                            Log.Default.Trace("Client:{0} Running", client);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        HttpCurrentContext.CloseAsTimeout();
                    }
                }
                else
                {
                    HttpCurrentContext.Close(HttpStatusCode.Forbidden, Error_HavePendingRequestAlready);
                }
            }
        }

        public void SendResult(string client)
        {
            Job job = GetOrCreateJob(client);
            using (AutoMonitor m = new AutoMonitor(job.ExecuteLock, MonitorTimeout))
            {
                if (m.Entered)
                {
                    if (job.Status == JobStatus.Running)
                    {
                        try
                        {
                            job.Message = HttpCurrentContext.Body;
                            HttpCurrentContext.Close();
                            job.Status = JobStatus.Finished;
                            Log.Default.Trace("Client:{0} Finished", client);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        HttpCurrentContext.Close(HttpStatusCode.Forbidden, string.Format("Current job status is: {0}", job.Status.ToString()));
                    }
                }
                else
                {
                    HttpCurrentContext.Close(HttpStatusCode.Forbidden, Error_HavePendingRequestAlready);
                }
            }
        }

        private Job GetOrCreateJob(string client)
        {
            lock (_clientsJob)
            {
                client = client.ToUpper();
                Job job = null;
                lock (_clientsJob)
                {
                    if (!_clientsJob.TryGetValue(client, out job))
                    {
                        job = new Job();
                        _clientsJob.Add(client, job);
                    }
                }
                return job;
            }
        }

        private bool Wait(Job job, JobStatus status, TimeSpan timeout)
        {
            DateTime start = DateTime.Now;
            int secs = (int)timeout.TotalSeconds;
            Log.Default.Trace("Wait job status timeout in:{0}", secs);
            while (DateTime.Now - start < timeout)
            {
                int passSeconds = (DateTime.Now - start).Seconds;
                if (job.Wait(status, CallerConnectCheckTime))
                {
                    return true;
                }
                //if (!HttpCurrentContext.Connected)
                //{
                //    job.Status = JobStatus.Idle;
                //    return false;
                //}
            }
            return false;
        }
    }
}
