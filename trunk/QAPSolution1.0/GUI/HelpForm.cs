using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI
{
    using System.Windows.Forms;
    using System.Drawing;

    public class HelpForm : Form    // inherits from System.Windows.Forms.Form
    {
        public HelpForm()
        {
            this.Text = "Pomoc";           // specify title of the form
            this.Width = 600;                         // width of the window in pixels
            this.Height = 600;                        // height in pixels
            RichTextBox box = new RichTextBox();
            box.Text = "Instrukcja obsługi programu\n\n"
            +"Pierwszym krokiem jest wczytanie macierzy dla których zostanie wyznaczone"
            + "rozwiązanie. Użytkownik może uczynić to poprzez naciśnięcie przycisku Wczytaj dane i wybraniu"
            + "pliku, który je zawiera.\n"
            + "Następnie należy wybrać odpowiednią zakładkę programu odpowiadającą algorytmowi"
            + "dla którego chcemy uzyskać obliczenia. Dzięki tej operacji zostanie umożliwione podanie"
            + "parametrów charakterystycznych dla danego algorytmu.\n"
            + "Kolejnym krokiem jest naciśnięcie przycisku Uruchom. Poskutkuje to utworzeniem okna"
            + "z wykresem na którym zwizualizowane zostaną rozwiązania problemu obliczone w kolejnych"
            + "iteracjach algorytmu.\n"
            + "Jeśli użytkownik chce zapisać otrzymane rozwiązania, powinien nacisnąć przycisk\n"
            + "Zapisz. Otrzyma w ten sposób plik z optymalnymi wynikami, który zostanie zapisany w wybranej"
            + "lokalizacji.\n\n"
            + "Tips&Tricks\n\n"
            + "Jeśli podczas przeglądania wykresu zastanawiasz się dla jakich danych algorytm"
            + "aktualnie wykonuje obliczenia, wystarczy spojrzeć na górny pasek wykresu, gdzie podana jest"
            + "ścieżka wczytanego pliku.";
            box.Dock = System.Windows.Forms.DockStyle.Fill;
            box.Font = new Font("Georgia", 12);
            box.ReadOnly = true;
            this.Controls.Add(box);
        }

        
    }
}
