using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeckerBoxForEyeGaze
{
    public partial class MainWindow : Window
    {

        private string[] btnUpperLeft = { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z" };
        private string[] btnUpperRight = { "9","8","7","6","5","4","3","2","1","0","Q","W","E","R","T","Y","U","I","O","P","A","S","D","F","G","H","J","K","L","Z","X","C","V","B","N","M","a","s","d","f",
            "g","h","j","k","l","A","S","D","F","G","H","J","K","L","[","]","+",";" };
        private string[] btnBotLeft = { "α", "β", "γ", "δ", "ϵ", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ" };
        private string[] btnBotRight = { "9", "8", "7", "6", "5", "4", "3", "2", "1", "0" };

        private string btnUpperLeftName = "lower-case alph";
        private string btnUpperRightName = "CAP + alph + number + sym";
        private string btnBotLeftName = "Special character";
        private string btnBotRightName = "integers";
    }
}
