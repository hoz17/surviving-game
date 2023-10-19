using System;
public interface ITakeDamage {
    public event EventHandler<OnTakeDamageEventArgs> OnTakeDamage;
    public class OnTakeDamageEventArgs : EventArgs {
        public float damage;
    }
}