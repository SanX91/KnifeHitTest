using System.Collections.Generic;

namespace KnifeHitTest
{
    /// <summary>
    /// The Log Breaker class.
    /// Implements the IBreaker interface.
    /// Responsible for breaking the log piece, when the stage ends successfully.
    /// Can be used for detailed break animations in the future.
    /// </summary>
    public class LogBreaker : IBreaker
    {
        private readonly ILogPiece logPiece;
        private readonly IEnumerable<Attachable> attachedItems;
        private readonly ILogData logData;

        public LogBreaker(ILogPiece logPiece, IEnumerable<Attachable> attachedItems, ILogData logData)
        {
            this.logPiece = logPiece;
            this.attachedItems = attachedItems;
            this.logData = logData;
        }

        /// <summary>
        /// Disables the log piece renderer and collider.
        /// Adds a random explosion force to the attached items of the log piece.
        /// </summary>
        public void Break()
        {
            logPiece.Renderer.enabled = logPiece.Collider.enabled = false;
            foreach (Attachable item in attachedItems)
            {
                item.transform.SetParent(null);
                item.AddExplosionForce(logData.ExplosionForce, logPiece.Position);
            }
        }

        public void Reset()
        {
            logPiece.Renderer.enabled = logPiece.Collider.enabled = true;
        }
    }
}
