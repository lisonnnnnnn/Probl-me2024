using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problème2024
{
    internal class De
    {
        private char[] sixFaces;
        private char faceVisible;

        public De(Random r, Alphabet alphabet)
        {
            sixFaces = new char[6];

            for (int i = 0; i < 6; i++)
            {
                int indiceLettreAleatoire = r.Next(alphabet.Possibilites.Count - 1);
                sixFaces[i] = alphabet.Possibilites[indiceLettreAleatoire];
                alphabet.Possibilites.RemoveAt(indiceLettreAleatoire);
            }
            Lance(r);
        }
        public char[] SixFaces
        {
            get { return sixFaces; }
        }
        public char FaceVisible
        {
            get { return faceVisible; }
        }
        public void Lance(Random r)
        {
            faceVisible = sixFaces[r.Next(5)];
        }
        public string toString()
        {
            return $" La face visible est {faceVisible} parmi les six lettres suivantes : {string.Join(" ", sixFaces)}";
        }
    }
}
