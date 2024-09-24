using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Model
{


    //posts Model
    public class PostsTestCasesContainer
    {
        public List<PostsTestcase> TestCases { get; set; }
    }

    public class PostsTestcase
    {
        public string TestcaseNo { get; set; }
        public PostsModel Data { get; set; }
    }
    public class PostsModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }


    //comments model
    public class CommentsTestCasesContainer
    {
        public List<CommentsTestcase> Testcases { get; set; }
    }

    public class CommentsTestcase
    {
        public string TestcaseNo { get; set; }
        public CommentsModel Data { get; set; }
    }
    public class CommentsModel
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }


}
