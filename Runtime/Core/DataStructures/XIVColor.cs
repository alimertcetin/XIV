using System;

namespace XIV.Core.DataStructures
{
    public struct XIVColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public XIVColor(float r, float g, float b, float a = 1f)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public static XIVColor red => new XIVColor(1f, 0f, 0f, 1f);
        public static XIVColor green => new XIVColor(0f, 1f, 0f, 1f);
        public static XIVColor blue => new XIVColor(0f, 0f, 1f, 1f);
        public static XIVColor white => new XIVColor(1f, 1f, 1f, 1f);
        public static XIVColor black => new XIVColor(0f, 0f, 0f, 1f);
        public static XIVColor yellow => new XIVColor(1f, 0.9215686f, 0.01568628f, 1f);
        public static XIVColor magenta => new XIVColor(1f, 0f, 1f, 1f);
        public static XIVColor gray => new XIVColor(0.5f, 0.5f, 0.5f, 1f);
        public static XIVColor cyan => new XIVColor(0.0f, 1f, 1f, 1f);
        public static XIVColor clear => new XIVColor(0f, 0f, 0f, 0f);

#if UNITY_ENGINE || UNITY_EDITOR
        public static implicit operator UnityEngine.Color(XIVColor c)
        {
            return new UnityEngine.Color(c.r, c.g, c.b, c.a);
        }
#endif

        public static implicit operator XIVColor(System.Drawing.Color c)
        {
            return new XIVColor(
                c.R / 255f,
                c.G / 255f,
                c.B / 255f,
                c.A / 255f
            );
        }

        public override bool Equals(object obj)
        {
            if (!(obj is XIVColor)) return false;
            XIVColor other = (XIVColor)obj;
            return r.Equals(other.r) && g.Equals(other.g) && b.Equals(other.b) && a.Equals(other.a);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(r, g, b, a);
        }

        public static bool operator ==(XIVColor lhs, XIVColor rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(XIVColor lhs, XIVColor rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}
