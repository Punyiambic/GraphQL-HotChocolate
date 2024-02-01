
namespace Product.QL.Models.Auth
{
    public class PublicKeyOutput
    {
        public List<PublicKey> keys { get; set; }
    }

    public class PublicKey
    {
        public string alg { get; set; }
        public string kty { get; set; }
        public string use { get; set; }
        public string n { get; set; }
        public string e { get; set; }
        public string kid { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
    }
}
