using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace ComplexExplorer
{
    public class FormulaLuiCauchy : ComplexForm
    {
        static Complex i = new Complex(0, 1);
        static Complex unuPe2iPi = 1 / (2 *i* Math.PI);
        static double ro = 2;
        static double ro2 = ro * ro;
        static double lat = ro + 0.1;

        Complex gamma(double t)
        {
            return Complex.setRoTheta(ro, t);
        }

        Complex f(Complex z)
        {
            return z * z * z + z + 1;
            //return 1/(z * z * z + z + 1);
        }
        void arataMod(int ii, int jj, Complex z)
        {
            int k = (int)(100 * z.Ro);
            setPixel(ii, jj, getColor(k));
        }
        void arataArg(int ii, int jj, Complex z)
        {
            int k = (int)(512 * (1 + z.Theta / Math.PI));
            setPixel(ii, jj, getColor(k));
        }
        Complex formCauchy(Complex zstar)
        {

            double a = 0, b = 2 * Math.PI;

            //calculam suma Riemann-Stieltjes
            //pentru g(z)=f(z)/(z-zstar)

            Complex suma = 0;
            Complex z0 = gamma(a);
            for (double t = a; t <= b; t += 0.01)
            {
                Complex z1 = gamma(t);
                suma += f(z1) * (z1 - z0) / (z1 - zstar);
                z0 = z1;
            }
            return unuPe2iPi * suma;
        }
        public override void makeImage()
        {

            setXminXmaxYminYmax(-lat, lat, -lat, lat);
            setAxis();
            int imax2 = imax / 2;
            int jmax2 = jmax / 2;
            for (int ii = imin; ii <= imax2; ii++)
            {
                for (int jj = jmin; jj <= jmax2; jj++)
                {
                    Complex z = getZ(2 * ii, 2 * jj);
                    if (z.Ro2 >= ro2) continue;

                    Complex fExact = f(z);
                    Complex fAprox = formCauchy(z);

                    arataArg(ii, jj, fExact);
                    arataMod(ii, jj + jmax2, fExact);

                    arataArg(ii + imax2, jj, fAprox);
                    arataMod(ii + imax2, jj + jmax2, fAprox);
                }
                if (!resetScreen()) return;
            }
            setAxis();
            resetScreen();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormulaLuiCauchy));
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Image = ((System.Drawing.Image)(resources.GetObject("picBox.Image")));
            // 
            // FormulaLuiCauchy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(285, 639);
            this.Name = "FormulaLuiCauchy";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }







}
