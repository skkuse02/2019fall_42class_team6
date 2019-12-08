// DB Handler
const mariadb = require('mariadb');
const vals = require('./consts.js');
// mariadb connection pool
const pool = mariadb.createPool({
    host: vals.DBHost, port:vals.DBPort,
    user: vals.DBUser, password: vals.DBPass,
    connectionLimit: 5
});
// handle the Database directly with query input
async function directquery(query){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal;');
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// User Table
// SearchID(user_id) => check user_id exist
async function SearchID(user_id){
    let conn,rows,result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT user_id FROM users WHERE user_id='`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        if (rows.length > 0) result = true;
        else result = false;
        return result;
    }
};
// RegisterID
// RegisterID(user_info) => Add new user to the table
async function RegisterID(user_id, password, name, address, email_address, payment_method, role){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO users(user_id, user_pw, user_name, address, email_address, payment_id, role) VALUES('`+user_id+`', PASSWORD('`+password+`'), '`+name+`', '`+address+`', '`+email_address+`', '`+payment_method+`', `+ role+`);`
        await conn.query(query);
        result = true;
    }
    catch(err){z
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};
// ModifyInfo
// ModifyInfo(user_id, changed_info) => Update user tuple
async function ModifyInfo(user_id, password, name, address, email_address, payment_method, role){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `UPDATE users SET user_name = '`+name+`', user_pw = PASSWORD('`+password+`'), address = '`+address+`', email_address = '`+email_address+`', payment_id = '`+payment_method+`', role = `+role+` WHERE user_id = '`+user_id+`';`
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};
// SetDefaultPayment
// SetDefaultPayment(user_id, payment_id) => Update payment_id
async function SetDefaultPayment(user_id, payment_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `UPDATE users SET payment_id = '`+payment_id+`' WHERE user_id = '`+user_id+`';`
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};
/*
// ResetPW
// ResetPW(user_id) => Reset Password to 0000
async function ResetPW(user_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `UPDATE users SET password = PASSWORD('0000') WHERE user_id = '`+user_id+`';`
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};*/
// CheckLogin
// CheckLogin(user_id, password) => Check the user is valid or not. return user information
async function CheckLogin(user_id, password){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT * FROM users WHERE user_id='`+user_id+`' AND user_pw = Password('`+password+`');`
        console.log(query);
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        if (rows.length > 0) result = rows;
        else result=false;
        return result;
    }
};
/*
// RemoveID
// RemoveID(user_id) => remove user
async function RemoveID(user_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM users WHERE user_id = '`+user_id+`';`
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}*/
/*
// UserRole
// UserRole(user_id) => user is seller or buyer (bool)
async function UserRole(user_id){
    let conn,rows,result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT role FROM users WHERE user_id='`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};*/
// Mdoel Table
// LoadModelList
// LoadModelList(user_id) => show the list of Model(room) with user_id
async function LoadModelList(user_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal;');
        var query = `SELECT model_id, roomname FROM model WHERE user_id='`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};
// AddModel
// AddModel(model_info) => Add model to table
async function AddModel(model_id, user_id, model_file, roomInfo_file, roomname){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO model(model_id, user_id, model_file, add_date, roomInfo_file, roomname) VALUES('`+model_id+`','`+user_id+`','`+model_file+`', SYSDATE(), '`+roomInfo_file+`', '`+roomname+`');`;
        console.log(query);
		await conn.query(query);
        result = true;
    }
    catch(err){
        console.log(err);
	    result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};
// GetModelfile
// GetModelfile(model_id) => get modelfile name with model_id
async function GetModelfile(model_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT model_file FROM model WHERE model_id='`+model_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};
/*
// RemoveModel
// RemoveModel(model) => remove model
async function RemoveModel(model_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM model WHERE model_id='`+model_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};*/
/*
// GetAddDate
// GetAddDate(model_id) => return the created date
async function GetAddDate(model_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT add_date FROM model WHERE model_id='`+model_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};*/
// GetRoomInfofile
// GetRoomInfofile(model_id) => return roomInfo.json filename
async function GetRoomInfofile(model_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT roomInfo_file FROM model WHERE model_id='`+model_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};
// GetRoomName
// GerRoomName(model_id) => return room name
async function GetRoomName(model_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT roomname FROM model WHERE model_id='`+model_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
};
// Product Table
// AddProduct
// AddProduct(______)
async function AddProduct(product_id, product_name, company, width, height, depth, color, category, price, descrip){
    product_id, product_name, company, width, height, depth, color, category, price, descrip
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO product(product_id, product_name, company, width, height, depth, color, category, price, descrip) VALUES('`+product_id+`','`+product_name+`','`+company+`', `+width+`, `+height+`,`+depth+`,'`+color+`', '`+category+`, `+price+`,'`+descrip+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
};
// SearchByCategory
// SearchByCategory(category) => return product_id list
async function SearchByCategory(category){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM product WHERE category = '`+category+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// SearchByName
// SearchByName(name) => return product_id by name
async function SearchByName(name){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM product WHERE product_name = '`+name+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// SearchByCompany
// SearchByCompany(company) => return product_id by company
async function SearchByCompany(company){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM product WHERE company = '`+company+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// RemoveProduct
// RemoveProduct(product_id) ==> Not used
async function RemoveProduct(product_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM product WHERE product_id = '`+product_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// GetProductSize
// GetProductSize(product_id) => return product width, height, depth
async function GetProductSize(product_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT width, height, depth FROM product WHERE product_id = '`+product_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetProductColor
// GetProductColor(product_id) => return productColor
async function GetProductColor(product_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT color FROM product WHERE product_id = '`+product_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetProductInfo
// GetProductInfo(product_id) => return product information
async function GetProductInfo(product_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT * FROM product WHERE product_id = '`+product_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetProductfile
// GetProductfile(product_id) => return file list related to the product
async function GetProductfile(product_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_file FROM product_file WHERE product_id = '`+product_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// Cart Table
// GetCartid
// GetCartid(user_id) => return cart_id of user
async function GetCartid(user_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT cart_id FROM cart WHERE user_id = '`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetProductListByCartid
// GetProductListByCartid(cart_id) => return products in the cart
async function GetProductListByCartid(cart_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM cart WHERE cart_id = '`+cart_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// AddProductToCart
// AddProductToCart(cart_id, user_id, product_id) => add product to cart
async function AddProductToCart(cart_id, user_id, product_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO cart(cart_id, user_id, product_id) VALUES(`+cart_id+`','`+user_id+`','`+product_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// RemoveProductFromCart
// RemoveProductFromCart(cart_id, product_id) => remove product from cart
async function RemoveProductFromCart(cart_id, product_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM cart WHERE cart_id = '`+cart_id+`' AND product_id = '`+product_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// CreateCart when user is assigned
// CreateCart
async function CreateCart(cart_id, user_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO cart(cart_id, user_id, product_id) VALUES('`+cart_id+`', '`+user_id+`', '';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// RemoveCart
async function RemoveCart(cart_id){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM cart WHERE cart_id = '`+cart_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// Purchase Table
// RegPurchase
async function RegPurchase(purchase_id, user_id, product_id, payment_id, addr, purchase_status, total_cost){
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO purchase(purchase_id, user_id, product_id, payment_id, addr, purchase_date, purchase_status, total_cost) VALUES('`+purchase_id+`', '`+user_id+`', '`+ product_id + `', '` + payment_id + `', '`+addr+`', `+ SYSDATE() + `, '`+purchase_status+`', '`+total_cost+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// CheckPurchaseStatus
async function CheckPurchaseStatus(purchase_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT purchase_status FROM purchase WHERE purchase_id = '`+purchase_id+`' LIMIT 1;`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetPurchaseProduct
async function GetPurchaseProduct(purchase_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM purchase WHERE purchase_id = '`+purchase_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetPurchaseDate
async function GetPurchaseDate(purchase_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT purchase_date FROM purchase WHERE purchase_id = '`+purchase_id+`' LIMIT 1;`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetPurchaseHistory
async function GetPurchaseHistory(user_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT purchase_id FROM purchase WHERE user_id = '`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// GetTotalCost
async function GetTotalCost(purchase_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT total_cost FROM purchase WHERE purchase_id = '`+purchase_id+`' LIMIT 1;`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// Payment Table
// CheckPaymentPW
async function CheckPaymentPW(payment_id, payment_pw){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT payment_id FROM payment WHERE payment_id = '`+payment_id+`' AND payment_pw = PASSWORD('`+payment_pw+`');`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        if (rows.length > 0) result = true;
        else result = false;
        return result;
    }
}
// GetPaymentList
async function GetPaymentList(user_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT payment_id, card_number FROM payment WHERE user_id = '`+user_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        if (rows.length > 0) result = rows;
        else result = false;
        return result;
    }
}
// GetPaymentInfo
async function GetPaymentInfo(payment_id){
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT * FROM payment WHERE payment_id = '`+payment_id+`';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// AddPayment
async function AddPayment(payment_id, user_id, card_company, card_number, valid_month,valid_year, CVC, payment_pw) {
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO payment(payment_id, user_id, card_company, card_number, valid_month, valid_year, CVC, payment_pw) VALUES('`+payment_id+`', '`+user_id+`', '`+card_company+`', '`+card_number+`', `+valid_month+`,`+valid_year+`,`+CVC+`, PASSWORD('`+payment_pw+`'));`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// RemovePayment
async function RemovePayment(payment_id) {
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM payment WHERE payment_id = '`+payment_id+ `';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        console.log(err);
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// Keyword Table
// GetProductListByKeyword
async function GetProductListByKeyword(keyword_id) {
    let conn, rows, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `SELECT product_id FROM keyword WHERE keyword_id = '`+keyword_id+ `';`;
        rows = await conn.query(query);
    }
    catch(err){
        throw err;
    }
    finally{
        if (conn) conn.end();
        result = rows;
        return result;
    }
}
// AddProductByKeyword
async function AddProductByKeyword(keyword_id, product_id) {
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `INSERT INTO keyword(keyword_id, product_id) VALUES('`+keyword_id+`','`+product_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}
// RemoveProductByKeyword
async function RemoveProductByKeyword(product_id) {
    let conn, result;
    try{
        conn = await pool.getConnection();
        conn.query('Use intereal');
        var query = `DELETE FROM keyword WHERE product_id = '`+product_id+`';`;
        await conn.query(query);
        result = true;
    }
    catch(err){
        result = false;
        throw err;
    }
    finally{
        if (conn) conn.end();
        return result;
    }
}

module.exports = {
    directquery : directquery,
    searchID : SearchID,
    registerID : RegisterID,
    modifyInfo : ModifyInfo,
    setDefaultPayment : SetDefaultPayment,
    //resetPW : ResetPW,
    checkLogin : CheckLogin,
    //removeID : RemoveID,
    //userRole : UserRole,
    loadModelList : LoadModelList,
    addModel : AddModel,
    getModelfile : GetModelfile,
    //removeModel : RemoveModel,
    //getAddDate : GetAddDate,
    getRoomInfofile : GetRoomInfofile,
    getRoomName : GetRoomName,
    addProduct : AddProduct,
    searchByCategory : SearchByCategory,
    searchByName : SearchByName,
    searchByCompany : SearchByCompany,
    removeProduct : RemoveProduct,
    getProductSize : GetProductSize,
    getProductColor : GetProductColor,
    getProductfile : GetProductfile,
    getProductInfo : GetProductInfo,
    getCartid : GetCartid,
    getProductListByCartid : GetProductListByCartid,
    addProductToCart : AddProductToCart,
    removeProductFromCart : RemoveProductFromCart,
    createCart : CreateCart,
    removeCart : RemoveCart,
    regPurchase : RegPurchase,
    checkPurchaseStatus : CheckPurchaseStatus,
    getPurchaseProduct : GetPurchaseProduct,
    getPurchaseDate : GetPurchaseDate,
    getPurchaseHistory : GetPurchaseHistory,
    getTotalCost : GetTotalCost,
    checkPaymentPW : CheckPaymentPW,
    getPaymentList : GetPaymentList,
    getPaymentInfo : GetPaymentInfo,
    addPayment : AddPayment,
    removePayment : RemovePayment,
    getProductListByKeyword : GetProductListByKeyword,
    addProductByKeyword : AddProductByKeyword,
    RemoveProductByKeyword : RemoveProductByKeyword
};
