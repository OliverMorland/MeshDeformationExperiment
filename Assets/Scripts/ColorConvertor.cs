using UnityEngine;


public class ColorConvertor
{
    public static Color HLStoRGB(float H, float L, float S)
    {

        float C = (1 - Mathf.Abs(2 * L - 1)) * S;

        float H_scaled = H * 6;

        float X = C * (1 - Mathf.Abs(H_scaled % 2 - 1));

        Color R1G1B1;
        if (H_scaled == null)
        {
            R1G1B1 = new Color(0, 0, 0);
        }
        else if (H_scaled >= 0 && H_scaled < 1) //Red
        {
            R1G1B1 = new Color(C, X, 0);
        }
        else if (H_scaled >= 1 && H_scaled < 2) //Yellow
        {
            R1G1B1 = new Color(X, C, 0);
        }
        else if (H_scaled >= 2 && H_scaled < 3) //Green
        {
            R1G1B1 = new Color(0, C, X);
        }
        else if (H_scaled >= 3 && H_scaled < 4) //Cyan
        {
            R1G1B1 = new Color(0, X, C);
        }
        else if (H_scaled >= 4 && H_scaled < 5) //Blue
        {
            R1G1B1 = new Color(X, 0, C);
        }
        else //Pink
        {
            R1G1B1 = new Color(C, 0, X);
        }

        float m = L - C / 2;

        return new Color(R1G1B1.r + m, R1G1B1.g + m, R1G1B1.b + m);

    }


    public static void RGBtoHLS(float R, float G, float B, out float H, out float L, out float S)
    {
        //Find maximum and min
        float max = Mathf.Max(R, G, B);
        float min = Mathf.Min(R, G, B);

        //Finding L
        L = (max + min) / 2;

        float diff = max - min;

        if (Mathf.Abs(diff) < 0.00001)
        {
            S = 0;
            H = 0;  // H is really undefined.
            return;
        }

        if (L <= 0.5)
        {
            S = diff / (max + min);
        }
        else
        {
            S = diff / (2 - max - min);
        }


        float r_dist = (max - R) / diff;
        float g_dist = (max - G) / diff;
        float b_dist = (max - B) / diff;

        if (R == max)
        {
            H = b_dist - g_dist;
        }
        else if (G == max)
        {
            H = 2 + r_dist - b_dist;
        }
        else
        {
            H = 4 + g_dist - r_dist;
        }

        //Convert 0 - 1
        H /= 6f;

        //Ensure is between 0-360
        if (H < 0)
        {
            H += 1f;
        }
        if (H > 1f)
        {
            H -= 1f;
        }
    }
}
