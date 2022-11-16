using DB_LAB_WEB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB_WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TablesController : ControllerBase {
        private readonly DatabaseManager _dbManager = DatabaseManager.Instance;


        [HttpGet]
        [ProducesResponseType(typeof(Response<List<Table>>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Get() {
            if (_dbManager.Database == null) {
                return BadRequest(new { error = "Database is not created yet" });
            }
            
            var tables = _dbManager.Database.Tables;
            var response = new Response<List<Table>> {
                Value = tables,
                Links = tables.ToDictionary(t => t.Name, t => $"/Tables/{t.Name}")
            };

            return Ok(response);
        }


        [HttpGet("{name}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Get(string name) {
            if (_dbManager.Database == null) {
                return BadRequest(new { error = "Database is not created yet" });
            }

            var table = _dbManager.Database.Tables.Find(t => t.Name.Equals(name));
            if (table == null) {
                return NotFound(new { error = $"There is no table named {name} in the database" });
            }

            var response = new Response<Table> {
                Value = table,
                Links = new Dictionary<string, string> {
                    { "updateTable", $"/Tables/{name}/{{newName}}" },
                    { "deleteTable", $"/Tables/{name}" },
                    { "columns", $"/Tables/{name}/Columns" },
                    { "rows", $"/Tables/{name}/Rows" },
                    { "addColumn", $"/Tables/{name}/Columns/{{columnName}}/{{columnType}}" },
                    { "addRow", $"/Tables/{name}/Rows" }
                }
            };

            if (table.Columns.Count > 0) {
                response.Links.Add("updateColumn", $"/Tables/{name}/Columns/{{oldColumnName}}/{{newColumnName}}");
                response.Links.Add("deleteColumn", $"/Tables/{name}/Columns/{{columnName}}");
            }
            
            if (table.Rows.Count > 0) {
                response.Links.Add("updateRow", $"/Tables/{name}/Rows/{{id}}");
                response.Links.Add("deleteRow", $"/Tables/{name}/Rows/{{id}}");
            }

            return Ok(response);
        }


        [HttpPost("{name}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Post(string name) {
            if (_dbManager.Database == null) {
                return BadRequest(new { error = "Database is not created yet" });
            }

            if (!_dbManager.AddTable(name)) {
                return BadRequest(new { error = $"Database {_dbManager.Database.Name} already contains the table named {name}" });
            }

            var table = _dbManager.GetTable(_dbManager.Database.Tables.Count - 1);

            var response = new Response<Table> {
                Value = table,
                Links = new Dictionary<string, string> {
                    { "updateTable", $"/Tables/{name}/{{newName}}" },
                    { "deleteTable", $"/Tables/{name}" },
                    { "columns", $"/Tables/{name}/Columns" },
                    { "rows", $"/Tables/{name}/Rows" },
                    { "addColumn", $"/Tables/{name}/Columns/{{columnName}}/{{columnType}}" },
                    { "addRow", $"/Tables/{name}/Rows" }
                }
            };

            return Ok(response);
        }


        [HttpPut("{oldName}/{newName}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Put(string oldName, string newName) {
            if (_dbManager.Database == null) {
                return BadRequest(new { error = "Database is not created yet" });
            }

            var table = _dbManager.Database.Tables.Find(t => t.Name.Equals(oldName));
            if (table == null) {
                return NotFound(new { error = $"There is no table named {oldName} in the database" });
            }

            var response = new Response<Table> {
                Value = table,
                Links = new Dictionary<string, string> {
                    { "updateTable", $"/Tables/{newName}/{{newName}}" },
                    { "deleteTable", $"/Tables/{newName}" },
                    { "columns", $"/Tables/{newName}/Columns" },
                    { "rows", $"/Tables/{newName}/Rows" },
                    { "addColumn", $"/Tables/{newName}/Columns/{{columnName}}/{{columnType}}" },
                    { "addRow", $"/Tables/{newName}/Rows" }
                }
            };

            if (table.Columns.Count > 0) {
                response.Links.Add("updateColumn", $"/Tables/{newName}/Columns/{{oldColumnName}}/{{newColumnName}}");
                response.Links.Add("deleteColumn", $"/Tables/{newName}/Columns/{{columnName}}");
            }

            if (table.Rows.Count > 0) {
                response.Links.Add("updateRow", $"/Tables/{newName}/Rows/{{id}}");
                response.Links.Add("deleteRow", $"/Tables/{newName}/Rows/{{id}}");
            }

            table.Name = newName;
            return Ok(table);
        }


        [HttpDelete("{name}")]
        [ProducesResponseType(typeof(Response<Table>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Delete(string name) {
            if (_dbManager.Database == null) {
                return BadRequest(new { error = "Database is not created yet" });
            }

            var table = _dbManager.Database.Tables.Find(t => t.Name.Equals(name));
            int id = _dbManager.Database.Tables.IndexOf(table);

            if (table == null) {
                return NotFound(new { error = $"There is no table named {name} in the database" });
            }

            var response = new Response<Table> {
                Value = table,
                Links = new Dictionary<string, string> {
                    { "tables", "/Tables" },
                    { "createTable", "/Tables/{name}" }
                }
            };

            _dbManager.DeleteTable(id);
            return Ok(table);
        }
    }
}
