using System;
using System.Linq;
using System.Collections.Generic;

namespace Advantage.API
{
    public class PaginatedResponse<T>
    {

        
        //constructor
        public PaginatedResponse(IEnumerable<T> data, int i, int len){
            Data = data.Skip((i - 1)).Take(len).ToList();
            Total = data.Count();
        }//end constructor


        public int Total {get; set;}

        public IEnumerable<T> Data { get; set;}


    }//end class

}//end namespace