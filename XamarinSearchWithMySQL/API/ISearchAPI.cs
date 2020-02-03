using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;

namespace XamarinSearchWithMySQL.API
{
    public interface ISearchAPI
    {

        [Get("/person")]
        Task<List<Model.Person>> GetPeople();

        [Post("/search")]
        Task<List<Model.Person>> SearchPerson([Body(BodySerializationMethod.UrlEncoded)]Dictionary<string,object>data);





    }
}