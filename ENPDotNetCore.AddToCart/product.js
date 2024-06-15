const tblProduct = "products";
const tblCart = "carts"
let productId = null;
getProductTable();

function readBlog() {
    let lst = getProducts();
    console.log(lst);
}
function createProduct(product, description, price) {
    const products = localStorage.getItem(tblProduct);
    console.log(products);
    //1 == '1'
    //1 === '1' check also data type
    //check the local storage if the tblProduct is have or not, if not , create arry. If the data exits, the string json data change into the object and assign to the array
    let lst = [];
    if (products !== null) {
        lst = JSON.parse(products);
    }

    const requestModel = {
        id: uuidv4(),
        product: product,
        description: description,
        price: price
    };

    lst.push(requestModel);

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblProduct, jsonProduct); // only accept string , not object
    successMessage("Saving Succesful !");
    clearControls();
}

function editProduct(id) {
    let lst = getProducts();

    const items = lst.filter(x => x.id == id);
    console.log(items);

    console.log(items.length);

    if (items.length == 0) {
        console.log("No data found");
        errorMessage("No data found !");
        return;
    }

    //return items[0];
    let item = items[0];
    productId = item.id;
    $('#txtProduct').val(item.product);
    $('#txtDescription').val(item.description);
    $('#txtPrice').val(item.price);
    $('#txtProduct').focus();
}

function updateProduct(id, product, description, price) {
    const lst = getProducts();

    const items = lst.filter(x => x.id == id);
    console.log(items);
    console.log(items.length);
    if (items.length == 0) {
        console.log("No data found");
        errorMessage("No data found !");
        return;
    }
    const item = items[0];
    item.product = product;
    item.description = description;
    item.price = price;

    const index = lst.findIndex(x => x.id == id);
    lst[index] = item;

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(tblProduct, jsonProduct); // only accept string , not object
    successMessage("updating successful");
    clearControls();

}
function deleteProduct(id) {
    confirmMessage("Are you sure you want to delete it ?").then(
        function (value) {
            let lst = getProducts();
            const items = lst.filter(x => x.id == id);
            console.log(items);
            console.log(items.length);
            if (items.length == 0) {
                console.log("No data found");
                return;
            }
            lst = lst.filter(x => x.id !== id);
            const jsonProduct = JSON.stringify(lst);
            localStorage.setItem(tblProduct, jsonProduct); // only accept string , not object

            successMessage("Deleting Successful .");
            getProductTable();
        }
    );
}

$('#btnSave').click(function () {
    const product = $('#txtProduct').val();
    const description = $('#txtDescription').val();
    const price = $('#txtPrice').val();
    if (productId === null) {
        createProduct(product, description, price);
    }
    else {
        updateProduct(productId, product, description, price);
        productId = null;
    }
    getProductTable();

})

function clearControls() {
    $('#txtProduct').val('');
    $('#txtDescription').val('');
    $('#txtPrice').val('');
    $('#txtProduct').focus();
}

function getProductTable() {
    const lst = getProducts();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = ` 
        <tr>
            <td>
            <button type="button" class="btn btn-warning" onclick="editProduct('${item.id}')">Edit</button>
            <button type="button" class="btn btn-danger" onclick="deleteProduct('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.product}</td>
            <td>${item.description}</td>
            <td>$${item.price}</td>
             <td>
            <button type="button" class="btn btn-warning" onclick="addToCart('${item.id}')">AddToCart</button>
            </td>
            
        </tr> 
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);// like innerHtml in javascript
}

function getCartTable() {
    const lst = getCart();
    let count = 0;
    //let totalPrice = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = ` 
        <tr>
            <td>${++count}</td>
            <td>${item.cartProductName}</td>
            <td>$${item.cartPrice}</td>
            <td>
                <div class="d-flex">
                    <button class="btn btn-warning" onclick="increaseQuantity(${item.cartId})"><i class="fa-solid fa-plus"></i></button>
                    <div style="width:30px; height:30px;" class="text-center d-flex justify-content-center align-items-center">${item.cartQuantity}</div>
                    <button class="btn btn-danger" onclick="decreaseQuantity(${item.cartId})"><i class="fa-solid fa-minus"></i></button>
                </div>
            </td>
            <td>$${item.cartPrice * item.cartQuantity}</td>
            <td>
            <button class="btn btn-danger" onclick="deleteCart(${item.cartId})"><i class="fa-solid fa-trash"></i></button>
            </td>
        </tr> 
        `;
        htmlRows += htmlRow;
    });

    $('#cartTable').html(htmlRows);// like innerHtml in javascript
}
//AddToCart
function addToCart(id) {
    const products = getProducts();
    const cart = getCart();

    const product = products.find(item => item.id == id);
    if (!product) {
        return;
    }

    const cartItem = cart.find(item => item.cartProductId === id);
    if (cartItem) {
        cartItem.cartQuantity += 1;
    }
    else {
        cart.push({
            cartId: cart.length > 0 ? cart[cart.length - 1].cartId + 1 : 1,
            cartProductId: product.id,
            cartProductName: product.product,
            cartPrice: product.price,
            cartQuantity: 1
        });
    }

    const jsonCart = JSON.stringify(cart);
    localStorage.setItem(tblCart, jsonCart);
    CalculateTotal();
    getCartTable();
}

//deleteCart
function deleteCart(id) {

    confirmMessage("Are you sure you want to delete it ?").then(
        function (value) {
            let lst = getCart();
            console.log(lst);
            console.log(lst.length);
            if (lst.length == 0) {
                console.log("No data found");
                return;
            }
            const cart = lst.filter(item => item.cartId !== id);
            const jsonCart = JSON.stringify(cart);
            localStorage.setItem(tblCart, jsonCart); // only accept string , not object
            successMessage("Deleting Successful .");
            CalculateTotal();
            getCartTable();
        }
    );
}

function increaseQuantity(id) {
    const cart = getCart();
    const cartItem = cart.find(item => item.cartId === id);
    if (cartItem) {
        cartItem.cartQuantity += 1;
        const jsonCart = JSON.stringify(cart);
        localStorage.setItem(tblCart, jsonCart); // only accept string , not object 
        CalculateTotal();
        getCartTable();
    }
}

function decreaseQuantity(id) {
    const cart = getCart();
    const cartItem = cart.find(item => item.cartId === id);
    if (cartItem) {
        if (cartItem.cartQuantity === 1) {
            deleteCart(id);
        }
        else {
            cartItem.cartQuantity -= 1;
            const jsonCart = JSON.stringify(cart);
            localStorage.setItem(tblCart, jsonCart); // only accept string , not object 
            CalculateTotal();
            getCartTable();
        }
    }
}

function CalculateTotal() {
    let cart = getCart();
    let totalPrice = 0;
    cart.forEach(item => {
        totalPrice += item.cartQuantity * item.cartPrice;
    })
    $('#totalPrice').text(`$ ${totalPrice.toFixed(2)}`);
}

function getCart() {
    const cart = localStorage.getItem(tblCart);
    console.log(cart);
    let lst = [];
    if (cart !== null) {
        lst = JSON.parse(cart);
    }
    return lst;
}

function getProducts() {
    const products = localStorage.getItem(tblProduct);
    console.log(products);
    let lst = [];
    if (products !== null) {
        lst = JSON.parse(products);
    }
    return lst;
}