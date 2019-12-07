var express = require('express');
var router = express.Router();
var mdbConn   = require('../js/mariaDBConn');
var bodyParser = require('body-parser')
var parser = bodyParser.urlencoded({ extended: false});
var fs = require('fs');
var path = require('path');
/* GET home page. */
router.post('/user', parser, function (req, res){
  if (req.body.function=='SearchID'){
    mdbConn.searchID(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RegisterID'){
    mdbConn.registerID(req.body.user_id, req.body.password, req.body.user_name, req.body.address, req.body.email_address, req.body.payment_method, req.body.role).then((result)=>{
      res.send(result);
      console.log('register ID');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='ModifyInfo'){
    mdbConn.modifyInfo(req.body.user_id, req.body.password, req.body.user_name, req.body.address, req.body.email_address, req.body.payment_method, req.body.role).then((result)=>{
      res.send(result);
      console.log('modifyInfo');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='ResetPW'){
    mdbConn.resetPW(req.body.user_id).then((result)=>{
      res.send(result);
      console.log('resetPW');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='CheckLogin'){
    mdbConn.checkLogin(req.body.user_id, req.body.password).then((result)=>{
      console.log(result);
      res.send(result);
      console.log('checkLogin');
    }).catch((errMsg)=>{
      console.log(errMsg);
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveID'){
    mdbConn.removeID(req.body.user_id).then((result)=>{
      req.send(result);
      console.log('removeID');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='UserRole'){
    mdbConn.userRole(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/model', function (req, res){
  if (req.body.function=='LoadModelList'){
    mdbConn.loadModelList(req.body.user_id).then((result)=>{
	    console.log(result);
      res.send(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='AddModel'){
    var query = `SELECT count(*) as cnt FROM model LIMIT 1;`;
    mdbConn.directquery(query).then((result)=>{
      var model_id = 'model_'+(result[0].cnt*1+1)*"";
      mdbConn.addModel(model_id, req.body.user_id, null, req.body.roomInfo_file, req.body.roomname).then((result)=>{
        res.send(result);
        console.log('addModel');
      }).catch((errMsg)=>{
        res.send(errMsg);
      });
    });
  };
  if (req.body.function=='GetModelfile'){
    mdbConn.getModelfile(req.body.model_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveModel'){
    mdbConn.removemodel(req.body.model_id).then((result)=>{
      res.send(result);
      console.log('removeModel');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetAddDate'){
    mdbConn.getAddDate(req.body.model_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetRoomInfofile'){
    mdbConn.getRoomInfofile(req.body.model_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetRoomName'){
    mdbConn.getRoomName(req.body.model_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/product', function (req, res){
  product_id, product_name, company, width, height, depth, color, category, price, descrip
  if (req.body.function=='AddProduct'){
    var query = `SELECT count(*) as cnt FROM product LIMIT 1;`;
    mdbConn.directquery(query).then((result)=>{
      var product_id = 'product_'+result[0].cnt;
      mdbConn.addProduct(product_id, req.body.product_name, req.body.company, req.body.width, req.body.height, req.body.depth, req.body.color, req.body.category, req.body.price, req.body.descrip).then((result)=>{
        res.send(result);
        console.log('addProduct');
      }).catch((errMsg)=>{
        res.send(errMsg);
      });
    });
  };
  if (req.body.function=='SearchByCategory'){
    mdbConn.searchByCategory(req.body.category).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='SearchByName'){
    mdbConn.searchByName(req.body.name).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='SearchByCompany'){
    mdbConn.searchByCompany(req.body.company).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveProduct'){
    mdbConn.removeProduct(req.body.product_id).then((result)=>{
      res.send(result);
      console.log("removeProduct");
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetProductSize'){
    mdbConn.getProductSize(req.body.product_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetProductColor'){
    mdbConn.getProductColor(req.body.product_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetProductfile'){
    mdbConn.getProductfile(req.body.product_id).then((result)=>{
      for(var i in result){
        var productfile = result[i].product_file;
        var filename = path.join(__dirname,'..','public','file',productfile);
        fs.readFile(filename,function(err,buf){
          console.log(buf);
        });
      };
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/cart', function (req, res){
  if (req.body.function=='GetCartid'){
    mdbConn.getCartid(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetProductListByCartid'){
    mdbConn.getProductListByCartid(req.body.cart_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='AddProductToCart'){
    mdbConn.addProductToCart(req.body.cart_id, req.body.user_id, req.body.product_id).then((result)=>{
      res.send(result);
      console.log("addProductToCart");
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveProductFromCart'){
    mdbConn.removeProductfromCart(req.body.cart_id, req.body.product_id).then((result)=>{
      res.send(result);
      console.log("removeProductFromCart");
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='CreateCart'){
    mdbConn.createCart(req.body.cart_id, req.body.user_id).then((result)=>{
      res.send(result);
      console.log("createCaret");
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveCart'){
    mdbConn.removeCart(req.body.cart_id, req.body.product_id).then((result)=>{
      res.send(result);
      console.log("removeCart");
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/purchase', function (req, res){
  if (req.body.function=='RegPurchase'){
    mdbConn.regPurchase(req.body.purchase_id, req.body.user_id, req.body.product_id, req.body.payment_id, req.body.addr, req.body.purchase_status, req.body.total_cost).then((result)=>{
      res.send(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='CheckPurchaseStatus'){
    mdbConn.checkPurchaseStatus(req.body.purchase_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetPurchaseProduct'){
    mdbConn.getPurchaseProduct(req.body.purchase_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetPurchaseDate'){
    mdbConn.getPurchaseDate(req.body.purchase_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetPurchaseHistory'){
    mdbConn.getPurchaseHistory(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetTotalCost'){
    mdbConn.getTotalCost(req.body.purchase_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/payment', function (req, res){
  if (req.body.function=='CheckPaymentPW'){
    mdbConn.checkPaymentPW(req.body.payment_id, req.body.payment_pw).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetPaymentList'){
    mdbConn.getPaymentList(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='GetPaymentInfo'){
    mdbConn.getPaymentInfo(req.body.user_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='AddPayment'){
    var query = `SELECT count(*) as cnt FROM payment LIMIT 1;`;
    mdbConn.directquery(query).then((result)=>{
      var payment_id = 'payment_'+result[0].cnt;
      mdbConn.addPayment(payment_id, req.body.user_id, req.body.card_company, req.body.card_number, req.body.valid_month, req.body.valid_year, req.body.CVC, req.body.payment_pw).then((result)=>{
        res.send(result);
        console.log('addPayment');
      }).catch((errMsg)=>{
        res.send(errMsg);
      });
    });
  };
  if (req.body.function=='RemovePayment'){
    mdbConn.removePayment(req.body.payment_id).then((result)=>{
      res.send(result);
      console.log('removePayment');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

router.post('/keyword', function (req, res){
  if (req.body.function=='GetProductListByKeyword'){
    mdbConn.getProductListByKeyword(req.body.keyword_id).then((result)=>{
      res.send(result);
      console.log(result);
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='AddProductByKeyword'){
    mdbConn.addProductByKeyword(req.body.keyword_id, req.body.product_id).then((result)=>{
      res.send(result);
      console.log('addProductByKeyword');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
  if (req.body.function=='RemoveProductByKeyword'){
    mdbConn.removeProductByKeyword(req.body.product_id).then((result)=>{
      res.send(result);
      console.log('removeProductByKeyword');
    }).catch((errMsg)=>{
      res.send(errMsg);
    });
  };
});

module.exports = router;
