const tblBlog = "blogs";
//createBlog();
//updateBlog("bfae3cb2-2b01-46b9-a4c6-607412a10a44","2222","2222","22222");
//deleteBlog("96318d5f-a10a-467f-8289-72b36b8d7f8e");
function read() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
}
function createBlog() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
    //1 == '1'
    //1 === '1' check also data type
    //check the local storage if the tblBlog is have or not, if not , create arry. If the data exits, the string json data change into the object and assign to the array
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }

    const requestModel = {
        id : uuidv4(),
        title: "test title",
        author: "test author",
        content: "test content"
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object
}

function updateBlog(id,title, author, content)
{
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }

    const items = lst.filter(x => x.id == id);
    console.log(items);
    console.log(items.length);
    if(items.length == 0){
        console.log("No data found");
        return;
    }
    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex( x => x.id == id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object
}

function deleteBlog(id)
{
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
    let lst = [];
    if(blogs !== null){
        lst = JSON.parse(blogs);
    }
    const items = lst.filter(x => x.id == id);
    console.log(items);
    console.log(items.length);
    if(items.length == 0){
        console.log("No data found");
        return;
    }
    lst  = lst.filter( x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object
}


function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
      (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
  }
  
