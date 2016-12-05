using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;

namespace eCommerce.WebUI.Controllers.Api
{
    public class CustomersController : ApiController
    {
        /****************** RESTFul Format ******************************************/
        /* Verbal           Controller                                              */
        /* Reminder         Function                    URL format                  */
        /****************************************************************************/
        /*   I              Index                       # GET /accounts
         *   Still          Show/Details                # GET /accounts/1
         *   Need           New                         # GET /accounts/new
         *   Coffee         Create                      # POST /accounts
         *   Even           Edit                        # GET /accounts/1/edit
         *   Until          Update                      # PATCH/PUT /accounts/1
         *   Dark           Destroy                     # DELETE /accounts/1
         ****************************************************************************/

        private static DataContext db = new DataContext();
        readonly CustomerRepository _customerRepository = new CustomerRepository(db);

        // GET api/customers
        public IEnumerable<Customer> Get()
        {
            return _customerRepository.GetAll().ToList();
        }

        // GET api/customers/5
        public Customer Get(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        // POST api/customers
        [HttpPost]              // Added by PW as preferred method. Microsoft suggest calling the function Post<Controller>
        public Customer Post(Customer customer)    // Convention dictates that the newly created entity is returned
        {
            if (!ModelState.IsValid )
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            // Add the Customer
            _customerRepository.Insert(customer);
            _customerRepository.Commit();
            return customer;
        }

        // PUT api/customers/5
        [HttpPut]              // Added by PW as preferred method. Microsoft suggest calling the function Put<Controller>
        public void Put(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _customerRepository.GetById(id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            customerInDb.CustomerName = customer.CustomerName;
            customerInDb.BirthDate = customer.BirthDate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            _customerRepository.Update(customerInDb);
            _customerRepository.Commit();

        }

        // DELETE api/customers/5
        [HttpDelete]              // Added by PW as preferred method. Microsoft suggest calling the function Delete<Controller>
        public void Delete(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _customerRepository.GetById(id);
            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _customerRepository.Delete(id);
            _customerRepository.Commit();
        }
    }
}