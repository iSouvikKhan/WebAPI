using API.Database;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        //[HttpGet]
        //public string Greet(string name)
        //{
        //    return "Hi " + name;
        ////https://localhost:44391/api/user?name=Tanya
        //}

        Contexts db = new Contexts();

        // api/user // in line no. 23 and 38 URL is same but URI is different
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return db.Users.ToList();
        }


        // api/user/3
        [HttpGet]
        public User GetAllUsers(int id) // here the id is coming from default route or routeTemplate of " WebApiConfig " class
        {
            return db.Users.Find(id);
        }

        // api/user // in line no. 23 and 38 URL is same but URI is different
        [HttpPost]
        public HttpResponseMessage AddUser(User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        // api/user
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id,User model)
        {
            try
            {
                if (id == model.UserId)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
                
            }
            catch (Exception ex)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        // api/user
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

    }
}
