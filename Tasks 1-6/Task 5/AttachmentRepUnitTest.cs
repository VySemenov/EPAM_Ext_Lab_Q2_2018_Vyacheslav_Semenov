namespace Task_5
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4;

    [TestClass]
    public class AttachmentRepUnitTest
    {
        public static void FillAttachmentsRepository(int n = 10)
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                RemoveAll();
            }

            for (int i = 0; i < n; i++)
            {
                rep.Save(new Attachment { Id = i });
            }
        }

        public static void RemoveAll()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                List<Attachment> list = new List<Attachment>(rep.GetAll());

                foreach (var m in list)
                {
                    rep.Delete(m.Id);
                }
            }
        }

        [TestMethod]
        public void AttachmentGetEmptyRandomIdTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            Random rnd = new Random();
            int testId = rnd.Next(-100, 100);
            Attachment attachment = rep.Get(testId);
            if (attachment != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AttachmentGetAllEmptyTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            List<Attachment> list = rep.GetAll();
            if (list.Count != 0)
            {
                Assert.Fail("list.Count != 0");
            }
        }

        [TestMethod]
        public void AttachmentSaveCountTest()
        {
            int countBefore = AttachmentsRepository.Instance.GetAll().Count;

            AttachmentsRepository.Instance.Save(new Attachment());
            AttachmentsRepository.Instance.Save(new Attachment());
            AttachmentsRepository.Instance.Save(new Attachment());

            List<Attachment> list = AttachmentsRepository.Instance.GetAll();
            if (list.Count != countBefore + 1)
            {
                Assert.Fail("list.Count != countBefore + 1");
            }
        }

        [TestMethod]
        public void AttachmentSaveCountTest2()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillAttachmentsRepository(n);

            List<Attachment> list = rep.GetAll();

            if (list.Count != n)
            {
                Assert.Fail("list.Count != n");
            }
        }

        [TestMethod]
        public void AttachmentSaveEqualsTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            int uniqueId = 111;
            Attachment attachment = new Attachment { Id = uniqueId };
            rep.Save(attachment);

            Attachment attachmentFromList = rep.Get(uniqueId);

            if (!attachment.Equals(attachmentFromList))
            {
                Assert.Fail("!Equals");
            }
        }

        [TestMethod]
        public void AttachmentSaveNullTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            if (rep.Save(null))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AttachmentGetNotEmptyTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            int n = 10;

            FillAttachmentsRepository(n);

            Attachment attachment = null;
            for (int i = 0; i < n; i++)
            {
                attachment = AttachmentsRepository.Instance.Get(i);
                if (attachment != null)
                {
                    if (attachment.Id != i)
                    {
                        Assert.Fail("attachment.Id != testId");
                    }
                }
                else
                {
                    Assert.Fail("attachment == null");
                }

                attachment = null;
            }
        }

        [TestMethod]
        public void AttachmentGetAllEqualsTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            List<Attachment> list = new List<Attachment>
            {
                new Attachment { Id = 1 },
                new Attachment { Id = 2 },
                new Attachment { Id = 3 },
                new Attachment { Id = 4 },
                new Attachment { Id = 5 },
                new Attachment { Id = 6 },
                new Attachment { Id = 7 },
            };

            foreach (var u in list)
            {
                rep.Save(u);
            }

            List<Attachment> attachmentsList = rep.GetAll();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(attachmentsList[i]))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void AttachmentDeleteEmptyTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void AttachmentDeleteNotEmptyTest()
        {
            AttachmentsRepository rep = AttachmentsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillAttachmentsRepository(n);

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
