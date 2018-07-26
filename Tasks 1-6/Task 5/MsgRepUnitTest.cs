namespace Task_5
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4;

    [TestClass]
    public class MsgRepUnitTest
    {
        public static void FillMessagesRepository(int n = 10)
        {
            MessagesRepository rep = MessagesRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                RemoveAll();
            }

            for (int i = 0; i < n; i++)
            {
                rep.Save(new Message { Id = i });
            }
        }

        public static void RemoveAll()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                List<Message> list = new List<Message>(rep.GetAll());

                foreach (var m in list)
                {
                    rep.Delete(m.Id);
                }
            }
        }

        [TestMethod]
        public void MessageGetEmptyRandomIdTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            Random rnd = new Random();
            int testId = rnd.Next(-100, 100);
            Message message = rep.Get(testId);
            if (message != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MessageGetAllEmptyTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            List<Message> list = rep.GetAll();
            if (list.Count != 0)
            {
                Assert.Fail("list.Count != 0");
            }
        }

        [TestMethod]
        public void MessageSaveCountTest()
        {
            int countBefore = MessagesRepository.Instance.GetAll().Count;

            MessagesRepository.Instance.Save(new Message());
            MessagesRepository.Instance.Save(new Message());
            MessagesRepository.Instance.Save(new Message());

            List<Message> list = MessagesRepository.Instance.GetAll();
            if (list.Count != countBefore + 1)
            {
                Assert.Fail("list.Count != countBefore + 1");
            }
        }

        [TestMethod]
        public void MessageSaveCountTest2()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            int n = 10;
            FillMessagesRepository(n);

            List<Message> list = rep.GetAll();

            if (list.Count != n)
            {
                Assert.Fail("list.Count != n");
            }
        }

        [TestMethod]
        public void MessageSaveEqualsTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            int uniqueId = 111;
            Message message = new Message { Id = uniqueId, Text = "TestMessage" };
            rep.Save(message);

            Message messageFromList = rep.Get(uniqueId);

            if (!message.Equals(messageFromList))
            {
                Assert.Fail("!Equals");
            }
        }

        [TestMethod]
        public void MessageSaveNullTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            if (rep.Save(null))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MessageGetNotEmptyTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            int n = 10;

            FillMessagesRepository(n);

            Message message = null;
            for (int i = 0; i < n; i++)
            {
                message = MessagesRepository.Instance.Get(i);
                if (message != null)
                {
                    if (message.Id != i)
                    {
                        Assert.Fail("message.Id != testId");
                    }
                }
                else
                {
                    Assert.Fail("message == null");
                }

                message = null;
            }
        }

        [TestMethod]
        public void MessageGetAllEqualsTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            List<Message> list = new List<Message>
            {
                new Message { Id = 1, Text = "1" },
                new Message { Id = 2, Text = "2" },
                new Message { Id = 3, Text = "3" },
                new Message { Id = 4, Text = "4" },
                new Message { Id = 5, Text = "1" },
                new Message { Id = 6, Text = "123" },
                new Message { Id = 7, Text = "2" },
            };

            foreach (var u in list)
            {
                rep.Save(u);
            }

            List<Message> messagesList = rep.GetAll();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(messagesList[i]))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void MessageDeleteEmptyTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MessageDeleteNotEmptyTest()
        {
            MessagesRepository rep = MessagesRepository.Instance;
            RemoveAll();

            int n = 10;
            FillMessagesRepository(n);

            for (int i = 0; i < n; i++)
            {
                if (!rep.Delete(i))
                {
                    Assert.Fail();
                }
            }

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }
    }
}
