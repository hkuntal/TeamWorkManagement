using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Text;


namespace HttpHandlers
{
    // Need to use IRequiresSessionState interface to get read/write access to the session
    class SessionHandler : IHttpHandler, IReadOnlySessionState
    {
        /// <summary>
        /// True if this handler can be reused between calls. That's cool if you don't have any class instance data. False if you'd rather get a fresh one. 
        /// </summary>
        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            // Reference http://www.hanselman.com/blog/ABoilerplateHttpHandler.aspx
            //Check the size of the session object

            var session = HttpContext.Current.Session;
            
            // Get the size in the session
            long totalSessionBytes = 0;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream m;
            if (session == null)
            {
                response.Write("<html><body><h1>The session is null </h1></body></html>");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<table><th>Key</th><th>Value</th><th>Size</th>");
            foreach (var obj in session)
            {
                m = new MemoryStream();
                b.Serialize(m, obj);
                totalSessionBytes += m.Length;
                sb.Append("<tr><td>" + obj + "</td> <td>" + session[obj.ToString()] + "  </td> <td>" + m.Length + "  </td></tr>");
            }
            sb.Append("</table>");

            response.Write("<html><body>");
            
            sb.Append("<h1>Total Session Size in Bytes is : " + totalSessionBytes);
            sb.Append("</h1>");

            //Get all the sessions in the system
            var count = GetCountOfAllSessionsInSystem();
            sb.Append(string.Format("<h1>Total no. Sessions in the System = {0}</h1>", count));
            sb.Append("</body></html>");

            response.Write(sb.ToString());
        }

        private int GetCountOfAllSessionsInSystem()
        {
            List<String> activeSessions = new List<String>();
            object obj = typeof(HttpRuntime).GetProperty("CacheInternal", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
            object[] obj2 = (object[])obj.GetType().GetField("_caches", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
            for (int i = 0; i < obj2.Length; i++)
            {
                Hashtable c2 = (Hashtable)obj2[i].GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2[i]);
                foreach (DictionaryEntry entry in c2)
                {
                    object o1 = entry.Value.GetType().GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry.Value, null);
                    if (o1.GetType().ToString() == "System.Web.SessionState.InProcSessionState")
                    {
                        SessionStateItemCollection sess = (SessionStateItemCollection)o1.GetType().GetField("_sessionItems", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o1);
                        if (sess != null)
                        {
                            if (sess["name"] != null)
                            {
                                activeSessions.Add(sess["name"].ToString());
                            }
                        }
                    }
                }
            }
            return activeSessions.Count;
        }
    }
}
