using System;
using System.Linq;
using System.Data.OleDb;
using ControlCambios.Clases;
using System.Data;

namespace ControlCambios.Core
{
    public class DBLayer
    {
        ErrorLog log = new ErrorLog();

        string conect = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "controlCambios.accdb";

        public DataView getAllRows()
        {
            try
            {
                controlCambiosDataSet dataset = new controlCambiosDataSet();

                controlCambiosDataSetTableAdapters.archivosTableAdapter adapter = new controlCambiosDataSetTableAdapters.archivosTableAdapter();

                adapter.Fill(dataset.archivos);

                if (dataset.archivos.Rows.Count.Equals(0))
                {
                    Registro.id = 1;
                }
                else
                {
                    Registro.id = dataset.archivos.Last().Id;
                }

                return dataset.archivos.DefaultView;
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.Message);

                return new DataView();
            }
        }

        public DataTable updateRows()
        {           
            try
            {
                DataTable dt = new DataTable();
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();

                string query = "select * from archivos";

                OleDbCommand cmd = new OleDbCommand(query, conexion);
                OleDbDataReader reader;

                reader = cmd.ExecuteReader();

                dt.Load(reader);
                reader.Close();
                conexion.Close();

                return dt;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message);

                return new DataTable();
            }
        }

        public DataTable SelectRow(int id)
        {
            DataTable dt = new DataTable();
            OleDbConnection conexion = new OleDbConnection(conect);
            conexion.Open();

            string query = "select * from archivos where Id = " + id;

            OleDbCommand cmd = new OleDbCommand(query, conexion);
            OleDbDataReader reader;

            reader = cmd.ExecuteReader();

            dt.Load(reader);
            reader.Close();
            conexion.Close();

            return dt;
        }

        public bool UpdateData(bool isPRE, int id)
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();

                string query = "update archivos set subido_PRE = @isPRE where Id = @id;";

                OleDbCommand cmd = new OleDbCommand(query, conexion);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@isPRE", isPRE);
                cmd.Parameters.AddWithValue("@id", id);


                cmd.ExecuteNonQuery();

                conexion.Close();

                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message);

                return false;
            }
        }

        public bool InsertData(string fileName, string description, bool isPRE)
        {
            try
            {
                OleDbConnection conexion = new OleDbConnection(conect);
                conexion.Open();

                string query = "select Id from archivos where Id = " + (Registro.id + 1);

                OleDbCommand cmd = new OleDbCommand(query, conexion);
                OleDbDataReader reader;

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Registro.id = Registro.id + 1;
                }

                reader.Close();

                string insertar = "INSERT INTO archivos VALUES ( @id, @nombre_archivo, @descripcion , @subidoPRE, @created_date, @edit_date)";

                cmd = new OleDbCommand(insertar, conexion);

                cmd.Parameters.AddWithValue("@id", (Registro.id + 1));
                cmd.Parameters.AddWithValue("@nombre_archivo", fileName);
                cmd.Parameters.AddWithValue("@descripcion", description);
                cmd.Parameters.AddWithValue("@subidoPRE", isPRE);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@created_date", DateTime.Now.Date);

                cmd.ExecuteNonQuery();

                conexion.Close();

                return true;
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.Message);

                return false;
            }
        }
    }
}
