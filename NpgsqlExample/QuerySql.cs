// auto-generated by sqlc at 30/04/2024 23:58 - do not edit
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace NpgsqlExample;
public class QuerySql(string connectionString)
{
    private static byte[] GetBytes(IDataRecord reader, int ordinal)
    {
        const int bufferSize = 100000;
        ArgumentNullException.ThrowIfNull(reader);
        var buffer = new byte[bufferSize];
        var(bytesRead, offset) = (0, 0);
        while (bytesRead < bufferSize)
        {
            var read = (int)reader.GetBytes(ordinal, bufferSize + bytesRead, buffer, offset, bufferSize - bytesRead);
            if (read == 0)
                break;
            bytesRead += read;
            offset += read;
        }

        if (bytesRead < bufferSize)
            Array.Resize(ref buffer, bytesRead);
        return buffer;
    }

    private const string GetAuthorSql = "SELECT id, name, bio FROM authors WHERE  id  =  @id  LIMIT  1  ";  
    public readonly record struct GetAuthorRow(long Id, string Name, string Bio);
    public readonly record struct GetAuthorArgs(long Id);
    public async Task<GetAuthorRow?> GetAuthor(GetAuthorArgs args)
    {
        await using var connection = NpgsqlDataSource.Create(connectionString);
        await using var command = connection.CreateCommand(GetAuthorSql);
        command.Parameters.AddWithValue("@id", args.Id);
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return new GetAuthorRow
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
            };
        return null;
    }

    private const string ListAuthorsSql = "SELECT id, name, bio FROM authors ORDER  BY  name  ";  
    public readonly record struct ListAuthorsRow(long Id, string Name, string Bio);
    public async Task<List<ListAuthorsRow>> ListAuthors()
    {
        await using var connection = NpgsqlDataSource.Create(connectionString);
        await using var command = connection.CreateCommand(ListAuthorsSql);
        await using var reader = await command.ExecuteReaderAsync();
        var rows = new List<ListAuthorsRow>();
        while (await reader.ReadAsync())
        {
            rows.Add(new ListAuthorsRow { Id = reader.GetInt64(0), Name = reader.GetString(1), Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2) });
        }

        return rows;
    }

    private const string CreateAuthorSql = "INSERT INTO authors ( name , bio ) VALUES ( @name, @bio ) RETURNING  id, name, bio ";  
    public readonly record struct CreateAuthorRow(long Id, string Name, string Bio);
    public readonly record struct CreateAuthorArgs(string Name, string Bio);
    public async Task<CreateAuthorRow?> CreateAuthor(CreateAuthorArgs args)
    {
        await using var connection = NpgsqlDataSource.Create(connectionString);
        await using var command = connection.CreateCommand(CreateAuthorSql);
        command.Parameters.AddWithValue("@name", args.Name);
        command.Parameters.AddWithValue("@bio", args.Bio);
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return new CreateAuthorRow
            {
                Id = reader.GetInt64(0),
                Name = reader.GetString(1),
                Bio = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
            };
        return null;
    }

    private const string DeleteAuthorSql = "DELETE FROM authors WHERE  id  =  @id  ";  
    public readonly record struct DeleteAuthorArgs(long Id);
    public async Task DeleteAuthor(DeleteAuthorArgs args)
    {
        await using var connection = NpgsqlDataSource.Create(connectionString);
        await using var command = connection.CreateCommand(DeleteAuthorSql);
        command.Parameters.AddWithValue("@id", args.Id);
        await command.ExecuteScalarAsync();
    }

    private const string TestSql = "SELECT c_bit, c_smallint, c_boolean, c_integer, c_bigint, c_serial, c_decimal, c_numeric, c_real, c_double_precision, c_date, c_time, c_timestamp, c_char, c_varchar, c_bytea, c_text, c_json FROM node_postgres_types LIMIT  1  ";  
    public readonly record struct TestRow(byte[]? C_bit, int? C_smallint, bool? C_boolean, int? C_integer, int? C_bigint, long? C_serial, float? C_decimal, float? C_numeric, float? C_real, float? C_double_precision, string C_date, string C_time, string C_timestamp, string C_char, string C_varchar, byte[]? C_bytea, string C_text, object? C_json);
    public async Task<TestRow?> Test()
    {
        await using var connection = NpgsqlDataSource.Create(connectionString);
        await using var command = connection.CreateCommand(TestSql);
        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return new TestRow
            {
                C_bit = reader.IsDBNull(0) ? null : GetBytes(reader, 0),
                C_smallint = reader.IsDBNull(1) ? null : reader.GetInt32(1),
                C_boolean = reader.IsDBNull(2) ? null : reader.GetBoolean(2),
                C_integer = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                C_bigint = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                C_serial = reader.IsDBNull(5) ? null : reader.GetInt64(5),
                C_decimal = reader.IsDBNull(6) ? null : reader.GetFloat(6),
                C_numeric = reader.IsDBNull(7) ? null : reader.GetFloat(7),
                C_real = reader.IsDBNull(8) ? null : reader.GetFloat(8),
                C_double_precision = reader.IsDBNull(9) ? null : reader.GetFloat(9),
                C_date = reader.IsDBNull(10) ? string.Empty : reader.GetString(10),
                C_time = reader.IsDBNull(11) ? string.Empty : reader.GetString(11),
                C_timestamp = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),
                C_char = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                C_varchar = reader.IsDBNull(14) ? string.Empty : reader.GetString(14),
                C_bytea = reader.IsDBNull(15) ? null : GetBytes(reader, 15),
                C_text = reader.IsDBNull(16) ? string.Empty : reader.GetString(16),
                C_json = reader.IsDBNull(17) ? null : reader.GetString(17)
            };
        return null;
    }
}