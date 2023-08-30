using Expenses.Models;
using Expenses.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Expenses.Controllers
{
    public class ChartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Chart() 
        {
            /*
            List<Chart> charts = new List<Chart>();
            Chart c1 = new Chart() { Movement = "Mercado", Value = 840.4 };
            Chart c2 = new Chart() { Movement = "Farmácia", Value = 200.4 };
            Chart c3 = new Chart() { Movement = "Transporte", Value = 150 };
            Chart c4 = new Chart() { Movement = "Mercado", Value = 400.0 };
            charts.Add(c1);
            charts.Add(c2);
            charts.Add(c3);
            charts.Add(c4);
            */
            List<object> charts = new List<object>();
            DataTable dt = new DataTable();
            dt.Columns.Add("Categoria", System.Type.GetType("System.String"));
            dt.Columns.Add("Valor", System.Type.GetType("System.Double"));

            DataRow dr = dt.NewRow();
            dr["Categoria"] = "Mercado";
            dr["Valor"] = 880.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Farmácia";
            dr["Valor"] = 180.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Bichos";
            dr["Valor"] = 250.0;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Categoria"] = "Transporte";
            dr["Valor"] = 136.0;
            dt.Rows.Add(dr);

            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                charts.Add(x);
            }

            return Json(charts);
        }

        [HttpPost]
        public JsonResult NovoGrafico()
        {
            List<object> iDados = new List<object>();
            //Criando dados de exemplo
            DataTable dt = new DataTable();
            dt.Columns.Add("Vendas", System.Type.GetType("System.String"));
            dt.Columns.Add("Unidades", System.Type.GetType("System.Int32"));

            DataRow dr = dt.NewRow();
            dr["Vendas"] = "Chevrolet Onix";
            dr["Unidades"] = 171;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Huinday HB20";
            dr["Unidades"] = 96;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Ford Ka(Hatch)";
            dr["Unidades"] = 87;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "WolksVagem Gol";
            dr["Unidades"] = 67;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Vendas"] = "Renaul Sandero";
            dr["Unidades"] = 63;
            dt.Rows.Add(dr);

            //Percorrendo e extraindo cada DataColumn para List<Object>
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iDados.Add(x);
            }
            //Dados retornados no formato JSON
            //return Json(iDados, JsonRequestBehavior.AllowGet);
            return Json(iDados);
        }
    }
}
