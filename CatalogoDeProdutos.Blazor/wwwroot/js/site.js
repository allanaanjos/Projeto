window.confirme = (message) => {
    return confirm(message);
};

window.showToast = () => {
    var toastEl = document.getElementById('toastProdutoCriado');
    var toast = new bootstrap.Toast(toastEl);
    toast.show();
};


