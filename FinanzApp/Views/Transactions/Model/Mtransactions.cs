﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Transactions.Model
{
	public class Mtransactions
	{
		public int ID { get; set; }
		public string Iduser { get; set; }
		public string Tipo { get; set; } // Ingreso o Egreso
		public DateTime Fecha { get; set; }
		public double Monto { get; set; }
		public string CategoriaID { get; set; } // ID de la categoría a la que pertenece la transacción
		public string Descripcion { get; set; }
	}
}
