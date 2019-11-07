using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class ImagineUtils
    {
        //Context
        //public Model1Container1 Context { get; set; }

        //Set context
        public ImagineUtils()
        {
            //Context = givenContext;
        }

        //ADD image
        public void AddImage(Imagine givenImagine)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenImagine != null)
                {
                    Context.Imagini.Add(givenImagine);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //GET full list of images
        public List<Imagine> GetImagesList()
        {
            using (Model1Container1 Context = new Model1Container1())
            { return Context.Imagini.ToList(); }
        }

        //GET single image by given index
        public Imagine GetImagine(int imageIndex)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                Imagine focusedImagine = null;
                if (imageIndex > 0)
                {
                    focusedImagine = Context.Imagini.FirstOrDefault(a => a.Id == imageIndex);
                }
                else
                {
                    throw new ArgumentException("Invalid image ID.");
                }
                return focusedImagine;
            }
        }

        //UPDATE given image
        public void UpdateImage(Imagine givenImagine)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenImagine != null)
                {
                    Imagine modifiedImagine = GetImagine(givenImagine.Id);

                    //Checking if the image by the given id exists
                    if (modifiedImagine != null)
                    {
                        modifiedImagine.Titlu = givenImagine.Titlu;
                        modifiedImagine.Descriere = givenImagine.Descriere;
                        modifiedImagine.Data = givenImagine.Data;
                        modifiedImagine.Foto = givenImagine.Foto;

                        Context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Nothing found at the given ID.");
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        //REMOVE given image
        public void RemoveImage(Imagine givenImagine)
        {
            using (Model1Container1 Context = new Model1Container1())
            {
                if (givenImagine != null)
                {
                    Context.Imagini.Remove(givenImagine);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
}
