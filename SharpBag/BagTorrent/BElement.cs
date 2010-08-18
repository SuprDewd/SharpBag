using System.Text;

namespace SharpBag.BagTorrent
{
    /// <summary>
    /// An interface for bencoded elements.
    /// </summary>
    public interface BElement
    {
        /// <summary>
        /// Generates the bencoded equivalent of the element.
        /// </summary>
        /// <returns>The bencoded equivalent of the element.</returns>
        string ToBencodedString();

        /// <summary>
        /// Generates the bencoded equivalent of the element.
        /// </summary>
        /// <param name="u">The StringBuilder to append to.</param>
        /// <returns>The bencoded equivalent of the element.</returns>
        StringBuilder ToBencodedString(StringBuilder u);
    }
}