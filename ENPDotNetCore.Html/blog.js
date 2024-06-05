const tblBlog = "blogs";
let blogId = null;
//createBlog();
//updateBlog("bfae3cb2-2b01-46b9-a4c6-607412a10a44","2222","2222","22222");
//deleteBlog("96318d5f-a10a-467f-8289-72b36b8d7f8e");
getBlogTable();

function readBlog() {
    let lst = getBlogs();
    console.log(lst);
}
function createBlog(title, author, content) {
    //1 == '1'
    //1 === '1' check also data type
    //check the local storage if the tblBlog is have or not, if not , create arry. If the data exits, the string json data change into the object and assign to the array
    let lst = getBlogs();
    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object

    successMessage("Saving Successful .");
    clearControls();
}

function editBlog(id) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id == id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("No data found");
        errorMessage("No data found");
        return;
    }

    //return items[0];
    let item = items[0];
    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();
}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id == id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("No data found");
        errorMessage("No data found");
        return;
    }
    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    const index = lst.findIndex(x => x.id == id);
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object

    successMessage("updating successful");
}

// function deleteBlog(id) {
//     let result = confirm("Are you sure to delete ?");
//     if(!result ) return;

//     let lst = getBlogs();
//     const items = lst.filter(x => x.id == id);
//     console.log(items);
//     console.log(items.length);
//     if (items.length == 0) {
//         console.log("No data found");
//         return;
//     }
//     lst = lst.filter(x => x.id !== id);
//     const jsonBlog = JSON.stringify(lst);
//     localStorage.setItem(tblBlog, jsonBlog); // only accept string , not object

//     successMessage("Deleting Successful .");
//     getBlogTable();
// }
function deleteBlog(id) {
    Notiflix.Confirm.show(
        ' Delete Confirmation',
        'Are you sure to delete?',
        'Yes',
        'No',
        function okCb() {
            let lst = getBlogs();
            const items = lst.filter(x => x.id == id);
            console.log(items);
            console.log(items.length);
            if (items.length == 0) {
                console.log("No data found");
                return;
            }
            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog); // only accept string, not object

            successMessage("Deleting Successful.");
            getBlogTable();
        },
        function cancelCb() {
            console.log('Deletion cancelled');
        },
        
    );
}



function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs);
    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    if(blogId === null){
        createBlog(title, author, content);
    }
    else{
        updateBlog(blogId,title,author,content);
        blogId = null;
    }
    getBlogTable();
    
})

function successMessage(message) {
    //alert(message);
    Swal.fire({
        title: "Success!",
        text: message,
        icon: "success"
    });
}

function errorMessage(message) {
    //alert(message);
    Swal.fire({
        title: "Error!",
        text: message,
        icon: "error"
    });
}

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = ` 
        <tr>
            <td>
            <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
            <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr> 
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);// like innerHtml in javascript
} 
