var cart = {
    init: function () {
        cart.regEvents();   
    },
    regEvents: function () {
   
        //Click nút cập nhật
        $('#capnhat').off('click').on('click', function () {
            //tạo một list các element có id = txtquantity
            var listProduct = $('.txtquantity');
            // khai báo mảng
            var cartList = [];
            //chạy vòng lặp
            $.each(listProduct, function (i, item) {
                //thêm từng phần tử vào mảng
                cartList.push({
                    //gán các thuộc tính của class Product_Cart_Session
                    Quantity: $(item).val(),
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                //gui len server
                url: '/Home/CartUpdate',
                //ten bien: cartmodel chuyen data thang chuoi bang json
                data: { cartmodel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Show_Cart";
                    }
                    else {
                        window.location.href = "/Home/Blank";
                    }
                 
                }
            });
        });
        //click nut xoa gio hang
        $('#xoagiohang').off('click').on('click', function () {
         
            $.ajax({
                //gui len server
                url: '/Home/DeleteAll_Cart',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Show_Cart";
                    }
                    else {
                        window.location.href = "/Home/Blank";
                    }

                }
            });
        });
        //click nut xoa tung sp
        $('button#xoasp').off('click').on('click', function () {

            $.ajax({
                //gui len server
                url: '/Home/DeleteProduct_Cart',
                data:{id : $(this).data('id')},
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Show_Cart";
                    }
                    else {
                        window.location.href = "/Home/Blank";
                    }

                }
            });
        });
        //Tang san pham
        $('button#tangsp').off('click').on('click', function () {

            $.ajax({
                //gui len server
                url: '/Home/Produc_Plus',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Show_Cart";
                    }
                    else {
                        window.location.href = "/Home/Blank";
                    }
                }
            });
        });
        //giam san pham
        $('button#giamsp').off('click').on('click', function () {

            $.ajax({
                //gui len server
                url: '/Home/Produc_Minus',
                data: { id: $(this).data('id') },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Show_Cart";
                    }
                    else {
                        window.location.href = "/Home/Blank";
                    }
                }
            });
        });
    }
}
cart.init();