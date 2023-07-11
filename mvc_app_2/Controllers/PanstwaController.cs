using Microsoft.AspNetCore.Mvc;
using mvc_app_2.Models;
using MySqlConnector;

namespace mvc_app_2.Controllers
{
    public class PanstwaController : Controller
    {
        private PanstwoRecordModel panstwoRecord;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Wyszukaj()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Wyszukaj(string panstwo)
        {
            string server = "localhost";
            string database = "projekt1";
            string username = "root";
            string password = "123123123";

            string connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Wykonaj operacje na bazie danych
                    string query = $"SELECT * FROM panstwa WHERE panstwo='{panstwo}';";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        panstwoRecord = new PanstwoRecordModel
                        {
                            panstwo = reader.GetString("panstwo"),
                            stolica = reader.GetString("stolica"),
                            duzeMiasto1 = reader.GetString("duze_miasto1"),
                            duzeMiasto2 = reader.GetString("duze_miasto2"),
                            duzeMiasto3 = reader.GetString("duze_miasto3")
                        };
                    }

                    reader.Close();
                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    // Obsłuż błąd połączenia
                    ViewBag.ErrorMessage = "Wystąpił błąd: " + ex.Message;
                }
            }
            //return RedirectToAction(nameof(Wyszukaj));
            //return View("WynikWyszuk", panstwoRecord);
            //return RedirectToAction("WynikWyszuk", panstwoRecord);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult WynikWyszuk(PanstwoRecordModel panstwoRecord)
        {
            return RedirectToAction(nameof(Index));
            //return View(panstwoRecord);
        }
    }
}
