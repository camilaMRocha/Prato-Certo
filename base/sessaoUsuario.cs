using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pratocerto
{
    class sessaoUsuario
    {


        public static int id { get; set; } // ID do usuário no banco         
        public static string nome { get; set; }// Nome do usuário ou restaurante     
        public static string email { get; set; }// Email do usuário    
        //public static int tipo { get; set; }// 0 = Cliente comum, 1 = Restaurante        
        public static string senha { get; set; }// Senha do usuário
        public static string telefone { get; set; }// telefone do restaurante
        public static string rua { get; set; }// rua do restaurante
        public static string foto { get; set; }// foto de usuário ou restaurante
<<<<<<< Updated upstream
        public static int status { get; set; }

=======

        public static string status { get; set; }
>>>>>>> Stashed changes

    }
}

