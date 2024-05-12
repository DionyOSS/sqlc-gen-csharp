// auto-generated by sqlc - do not edit
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace NpgsqlExample
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Npgsql;

    public class QuerySql
    {
        public QuerySql(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private string connectionString { get; }

        private const string GetAuthorSql = "SELECT id, name, bio FROM authors WHERE  id  =  @id  LIMIT  1  ";  
        public class GetAuthorRow
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
        };
        public class GetAuthorArgs
        {
            public long Id { get; set; }
        };
        public async Task<GetAuthorRow> GetAuthor(GetAuthorArgs args)
        {
            {
                using (var connection = NpgsqlDataSource.Create(connectionString))
                {
                    using (var command = connection.CreateCommand(GetAuthorSql))
                    {
                        command.Parameters.AddWithValue("@id", args.Id);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new GetAuthorRow
                                {
                                    Id = reader.GetInt64(0),
                                    Name = reader.GetString(1),
                                    Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                                };
                            }
                        }
                    }
                }

                return null;
            }
        }

        private const string ListAuthorsSql = "SELECT id, name, bio FROM authors ORDER  BY  name  ";  
        public class ListAuthorsRow
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
        };
        public async Task<List<ListAuthorsRow>> ListAuthors()
        {
            {
                using (var connection = NpgsqlDataSource.Create(connectionString))
                {
                    using (var command = connection.CreateCommand(ListAuthorsSql))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            var result = new List<ListAuthorsRow>();
                            while (await reader.ReadAsync())
                            {
                                result.Add(new ListAuthorsRow { Id = reader.GetInt64(0), Name = reader.GetString(1), Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2) });
                            }

                            return result;
                        }
                    }
                }
            }
        }

        private const string CreateAuthorSql = "INSERT INTO authors ( name , bio ) VALUES ( @name, @bio ) RETURNING  id, name, bio ";  
        public class CreateAuthorRow
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
        };
        public class CreateAuthorArgs
        {
            public string Name { get; set; }
            public string Bio { get; set; }
        };
        public async Task<CreateAuthorRow> CreateAuthor(CreateAuthorArgs args)
        {
            {
                using (var connection = NpgsqlDataSource.Create(connectionString))
                {
                    using (var command = connection.CreateCommand(CreateAuthorSql))
                    {
                        command.Parameters.AddWithValue("@name", args.Name);
                        command.Parameters.AddWithValue("@bio", args.Bio);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new CreateAuthorRow
                                {
                                    Id = reader.GetInt64(0),
                                    Name = reader.GetString(1),
                                    Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                                };
                            }
                        }
                    }
                }

                return null;
            }
        }

        private const string DeleteAuthorSql = "DELETE FROM authors WHERE  id  =  @id  ";  
        public class DeleteAuthorArgs
        {
            public long Id { get; set; }
        };
        public async Task DeleteAuthor(DeleteAuthorArgs args)
        {
            {
                using (var connection = NpgsqlDataSource.Create(connectionString))
                {
                    using (var command = connection.CreateCommand(DeleteAuthorSql))
                    {
                        command.Parameters.AddWithValue("@id", args.Id);
                        await command.ExecuteScalarAsync();
                    }
                }
            }
        }

        private const string TestSql = "SELECT c_bit, c_smallint, c_boolean, c_integer, c_bigint, c_serial, c_decimal, c_numeric, c_real, c_double_precision, c_date, c_time, c_timestamp, c_char, c_varchar, c_bytea, c_text, c_json FROM node_postgres_types LIMIT  1  ";  
        public class TestRow
        {
            public byte[] C_bit { get; set; }
            public int? C_smallint { get; set; }
            public bool? C_boolean { get; set; }
            public int? C_integer { get; set; }
            public int? C_bigint { get; set; }
            public long? C_serial { get; set; }
            public float? C_decimal { get; set; }
            public float? C_numeric { get; set; }
            public float? C_real { get; set; }
            public float? C_double_precision { get; set; }
            public string C_date { get; set; }
            public string C_time { get; set; }
            public string C_timestamp { get; set; }
            public string C_char { get; set; }
            public string C_varchar { get; set; }
            public byte[] C_bytea { get; set; }
            public string C_text { get; set; }
            public object C_json { get; set; }
        };
        public async Task<TestRow> Test()
        {
            {
                using (var connection = NpgsqlDataSource.Create(connectionString))
                {
                    using (var command = connection.CreateCommand(TestSql))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new TestRow
                                {
                                    C_bit = reader.IsDBNull(0) ? null : Utils.GetBytes(reader, 0),
                                    C_smallint = reader.IsDBNull(1) ? (int? )null : reader.GetInt32(1),
                                    C_boolean = reader.IsDBNull(2) ? (bool? )null : reader.GetBoolean(2),
                                    C_integer = reader.IsDBNull(3) ? (int? )null : reader.GetInt32(3),
                                    C_bigint = reader.IsDBNull(4) ? (int? )null : reader.GetInt32(4),
                                    C_serial = reader.IsDBNull(5) ? (long? )null : reader.GetInt64(5),
                                    C_decimal = reader.IsDBNull(6) ? (float? )null : reader.GetFloat(6),
                                    C_numeric = reader.IsDBNull(7) ? (float? )null : reader.GetFloat(7),
                                    C_real = reader.IsDBNull(8) ? (float? )null : reader.GetFloat(8),
                                    C_double_precision = reader.IsDBNull(9) ? (float? )null : reader.GetFloat(9),
                                    C_date = reader.IsDBNull(10) ? string.Empty : reader.GetString(10),
                                    C_time = reader.IsDBNull(11) ? string.Empty : reader.GetString(11),
                                    C_timestamp = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
                                    C_char = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                                    C_varchar = reader.IsDBNull(14) ? string.Empty : reader.GetString(14),
                                    C_bytea = reader.IsDBNull(15) ? null : Utils.GetBytes(reader, 15),
                                    C_text = reader.IsDBNull(16) ? string.Empty : reader.GetString(16),
                                    C_json = reader.IsDBNull(17) ? null : reader.GetString(17)
                                };
                            }
                        }
                    }
                }

                return null;
            }
        }
    }
}