using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using WebApiNuveo.Models;

namespace WebApiNuveo.Controllers
{
    public class WorkflowController : ApiController
    {
        private static List<Workflow> MyWorkflow = new List<Workflow>();
        private context db = new context();

        [HttpGet]
        public List<Workflow> getMylist()
        {
            var mylist = db.WorkFlow.ToList();
            return mylist;
        }

        [HttpPost]
        public HttpResponseMessage postMyWorkflow([FromBody]Workflow values)
        {
            try
            {
                Workflow obj = new Workflow();
                obj.steps = values.steps;
                obj.data = values.data;
                if (values.status == 1)
                    obj.status = (int)Enumera.WorkflowEnums.inserted;
                else if (values.status == 2)
                    obj.status = (int)Enumera.WorkflowEnums.consumed;
                else
                    obj.status = 0;

                obj.UUID = Guid.NewGuid();

                db.WorkFlow.Add(obj);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, obj);
                
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        [HttpPatch]
        public IHttpActionResult updateStatus([FromBody] Workflow values)
        {
            var item = db.WorkFlow.FirstOrDefault(x => x.UUID == values.UUID);
            
            if (item != null)
            {
                if (item.status == 1)
                    item.status = (int)Enumera.WorkflowEnums.consumed;
                else if (item.status == 2)
                    item.status = (int)Enumera.WorkflowEnums.inserted;
                else
                    item.status = 0;

                db.SaveChanges();

                return Ok();
            }

            return NotFound();
        }

        [Route("Workflow/consume")]
        [HttpGet]
        public HttpResponseMessage consume()
        {
            var mylist = db.WorkFlow.ToList();

            var sb = new StringBuilder();

            sb.Append("UUID,Status,Data,Steps\r\n");

            foreach (var item in mylist)
            {
                sb.AppendFormat("\"{0}\",", item.UUID.ToString());
                sb.AppendFormat("\"{0}\",", item.status.ToString());
                sb.AppendFormat("\"{0}\",", item.data);
                sb.AppendFormat("\"{0}\"\r\n", item.steps); 
            }

            HttpResponseMessage myReturn = new HttpResponseMessage(HttpStatusCode.OK);

            myReturn.Content = new StringContent(sb.ToString());
            myReturn.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            myReturn.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            myReturn.Content.Headers.ContentDisposition.FileName = "Workflow.csv";

            return myReturn;

        }
    }
}
