using Microsoft.VisualStudio.TestTools.UnitTesting;
using group_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group_project
{


    [TestClass()]
    public class DogWindowTests 
    {

        List<NoteWindow> notes;
        DogWindow dog;
        NoteStorage note_storage;

        [TestMethod()]
        public void DogWindowTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void save_noteTest()
        {
            note_storage.new_note();
            //Assert.AreEqual
            Assert.Fail();
        }

        [TestMethod()]
        public void new_noteTest()
        {
            note_storage.new_note();
            Assert.Fail();
        }

        [TestMethod()]
        public void delete_noteTest()
        {
            
            note_storage.delete_note("note1.osl");
            Assert.Fail();
        }
    }
}