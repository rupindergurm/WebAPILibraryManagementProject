using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LM.Controllers
{
    [RoutePrefix("api/Library")]
    public class LibraryController : ApiController
    {
        private readonly UserContext _context;
        public LibraryController()
        {
            _context = new UserContext();
        }
        [HttpGet]
        [Route("get/id")]
        public IHttpActionResult GetUser(Guid Id)
        {
            UserPoco poco = _context.Users.Where(u => u.id == Id).FirstOrDefault();
            return Ok(poco);
        }
        [HttpGet]
        [Route("get")]
        public IHttpActionResult GetUser()
        {
            UserPoco poco = _context.Users.FirstOrDefault();
            return Ok(poco);
        }
        [HttpPost]
        [Route("Post")]
        public IHttpActionResult PostUser([FromBody] UserPoco poco)
        {
            _context.Users.Add(poco);
            _context.SaveChanges();

            return Ok();
        }
        [HttpPut]
        [Route("put")]
        public IHttpActionResult PutUser([FromBody] UserPoco poco)
        {
            _context.Users.Append(poco);
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        [Route("delete")]
        public IHttpActionResult DeleteUser(Guid id)
        {
            UserPoco poco = _context.Users.Where(u => u.id == id).FirstOrDefault();
            _context.Users.Remove(poco);
            _context.SaveChanges();
            return Ok();
        }
    }
    public class UserContext : DbContext

    {
        public DbSet<BookPoco> Books { get; set; }
        public DbSet<UserPoco> Users { get; set; }

        public UserContext()
        {
            Database.Log = l => Debug.WriteLine(l);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPoco>().HasMany(u => u.Books);
            base.OnModelCreating(modelBuilder);
        }
    }
        [Table("Books")]
public class BookPoco
    { 
        [Key]
        public Guid id { get; set; }
        public String BookName { get; set; }
        public int ISBN { get; set; }
        public DateTime Published_Date { get; set; }
    }
    [Table("Users")]
    public class UserPoco
    {
        [Key]
        public Guid id { get; set; }
        public String UserName { get; set; }
        public DateTime DoB { get; set; }
        public virtual ICollection<BookPoco> Books { get; set; }
    }
}
