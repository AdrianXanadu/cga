using NUnit.Framework;
using CGA_Client.Utils;

namespace CGA_Testing
{
    public class ClientTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("1234", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4")]
        [TestCase("schokolade", "0932f91eafea248b0ce8e0140c85322eee6abac2a04acd97d4c48254c0d72123")]
        [TestCase("password", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")]
        public void PasswordHashEqual(string value1, string value2)
        {
            Assert.AreEqual(Cryptology.SHA256Hash(value1), value2);
        }

        [TestCase("tramvaj", "e67353eb7b2580daf4f704ea9ed3d74248b0fcd327f2931e3ed63534d40acd8f")]
        [TestCase("sommelier", "feebfe5d026f2e10ba8783a5c9bd3477590e6e4afb506c7ed583d8988637e95f")]
        [TestCase("trottoir", "f21fe7cc28fa9459425aa996e4807fa55a0741074f647bace1060a70af3f19b9")]
        public void PasswordHashUnequal(string value1, string value2)
        {
            Assert.AreNotEqual(Cryptology.SHA256Hash(value1), value2);
        }
    }
}