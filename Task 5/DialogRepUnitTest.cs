namespace Task_5
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task4;

    [TestClass]
    public class DialogRepUnitTest
    {
        public static void FillDialogsRepository(int n = 10)
        {
            DialogsRepository rep = DialogsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                RemoveAll();
            }

            for (int i = 0; i < n; i++)
            {
                rep.Save(new Dialog { Id = i });
            }
        }

        public static void RemoveAll()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            if (rep.GetAll().Count > 0)
            {
                List<Dialog> list = new List<Dialog>(rep.GetAll());

                foreach (var m in list)
                {
                    rep.Delete(m.Id);
                }
            }
        }

        [TestMethod]
        public void DialogGetEmptyRandomIdTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            Random rnd = new Random();
            int testId = rnd.Next(-100, 100);
            Dialog dialog = rep.Get(testId);
            if (dialog != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DialogGetAllEmptyTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            List<Dialog> list = rep.GetAll();
            if (list.Count != 0)
            {
                Assert.Fail("list.Count != 0");
            }
        }

        [TestMethod]
        public void DialogSaveCountTest()
        {
            int countBefore = DialogsRepository.Instance.GetAll().Count;

            DialogsRepository.Instance.Save(new Dialog());
            DialogsRepository.Instance.Save(new Dialog());
            DialogsRepository.Instance.Save(new Dialog());

            List<Dialog> list = DialogsRepository.Instance.GetAll();
            if (list.Count != countBefore + 1)
            {
                Assert.Fail("list.Count != countBefore + 1");
            }
        }

        [TestMethod]
        public void DialogSaveCountTest2()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillDialogsRepository(n);

            List<Dialog> list = rep.GetAll();

            if (list.Count != n)
            {
                Assert.Fail("list.Count != n");
            }
        }

        [TestMethod]
        public void DialogSaveEqualsTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            int uniqueId = 111;
            Dialog dialog = new Dialog { Id = uniqueId };
            rep.Save(dialog);

            Dialog dialogFromList = rep.Get(uniqueId);

            if (!dialog.Equals(dialogFromList))
            {
                Assert.Fail("!Equals");
            }
        }

        [TestMethod]
        public void DialogSaveNullTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            if (rep.Save(null))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DialogGetNotEmptyTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            int n = 10;

            FillDialogsRepository(n);

            Dialog dialog = null;
            for (int i = 0; i < n; i++)
            {
                dialog = DialogsRepository.Instance.Get(i);
                if (dialog != null)
                {
                    if (dialog.Id != i)
                    {
                        Assert.Fail("dialog.Id != testId");
                    }
                }
                else
                {
                    Assert.Fail("dialog == null");
                }

                dialog = null;
            }
        }

        [TestMethod]
        public void DialogGetAllEqualsTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            List<Dialog> list = new List<Dialog>
            {
                new Dialog { Id = 1 },
                new Dialog { Id = 2 },
                new Dialog { Id = 3 },
                new Dialog { Id = 4 },
                new Dialog { Id = 5 },
                new Dialog { Id = 6 },
                new Dialog { Id = 7 },
            };

            foreach (var u in list)
            {
                rep.Save(u);
            }

            List<Dialog> dialogsList = rep.GetAll();

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].Equals(dialogsList[i]))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void DialogDeleteEmptyTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            if (rep.Delete(1))
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DialogDeleteNotEmptyTest()
        {
            DialogsRepository rep = DialogsRepository.Instance;
            RemoveAll();

            int n = 10;
            FillDialogsRepository(n);

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
