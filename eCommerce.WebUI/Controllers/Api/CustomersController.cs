using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using eCommerce.Model;
using eCommerce.Model.Dtos;

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
        public IHttpActionResult Get()
        {
            // Old method of using domain model as return value. Bad Practice
            //return _customerRepository.GetAll().ToList();

            // Method returning IEnumerable<CustomerDto>, custom DTO, which is considered better practice
            //return _customerRepository.GetAll().ToList().Select(Mapper.Map<Customer, CustomerDto>);

            // Best practice. Returning the correct Http Status Code, and in this case 
            // a list from our model mapped to a DTO
            return Ok(_customerRepository.GetAll().ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        // GET api/customers/5
        public IHttpActionResult Get(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                return BadRequest();
            //throw new HttpResponseException(HttpStatusCode.NotFound); // Old Code

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
            //return Mapper.Map<Customer, CustomerDto>(customer);   // Old Code
        }

        // POST api/customers
        [HttpPost]              // Added by PW as preferred method. Microsoft suggest calling the function Post<Controller>
        public IHttpActionResult Post(CustomerDto customerDto)    // Convention dictates that the newly created entity is returned
                                                            // But instead of simply returning the DTO, we should
                                                            // use a Http helper to return the correct return code
                                                            // 201 if created successfully, as well as the URI to the new
                                                            // entity, and also the DTO or Model itself
        {
            if (!ModelState.IsValid )
                return BadRequest();
            // throw new HttpResponseException(HttpStatusCode.BadRequest);  // Old Code

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);  // Use AutoMApper

            // Add the Customer
            _customerRepository.Insert(customer);
            _customerRepository.Commit();

            // Get the Id from the newly formed Customer
            customerDto.CustomerId = customer.CustomerId;
            return Created(new Uri(Request.RequestUri + "/" + customer.CustomerId), customerDto);
        }

        // PUT api/customers/5
        [HttpPut]              // Added by PW as preferred method. Microsoft suggest calling the function Put<Controller>
        public IHttpActionResult Put(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)  
                return BadRequest();              
                //throw new HttpResponseException(HttpStatusCode.BadRequest);   // Old code

            Customer customerInDb = _customerRepository.GetById(id);
            if (customerInDb == null)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.NotFound);   // Old Code

            // Map the Customer DTO to the found Customer entity in the database.
            // Because we are supplying both souce and destination objects, AutoMapper
            // knows what types we are using, so instead of the following:-
            // Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            // We can write as follows:-
            Mapper.Map(customerDto, customerInDb);

            _customerRepository.Update(customerInDb);
            _customerRepository.Commit();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/customers/5
        [HttpDelete]              // Added by PW as preferred method. Microsoft suggest calling the function Delete<Controller>
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
                //throw new HttpResponseException(HttpStatusCode.BadRequest);   // Old Code

            var customerInDb = _customerRepository.GetById(id);
            if (customerInDb == null)
                return NotFound();
                //throw new HttpResponseException(HttpStatusCode.NotFound);   // Old Code

            _customerRepository.Delete(id);
            _customerRepository.Commit();

            return StatusCode(status:HttpStatusCode.NoContent);            
        }
    }
}