
namespace FinalExam.ViewModel
{
    class ViewModelLocator
    {
        public ViewModelLocator()
        {
            MainVM = new MainViewModel();
        }

        public MainViewModel MainVM { get; }
    }
}
