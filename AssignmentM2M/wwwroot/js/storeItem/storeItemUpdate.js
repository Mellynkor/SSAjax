"use strict";
(function _storeItemUpdate() {
    const formUpdate =
        document.querySelector("#formUpdate");
    formUpdate.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/storeitemapi/update";
        const method = "put";
        const formData = new FormData(formUpdate);
        const storecode = formData.get("StoreCode");
        console.log(`${url} ${method}`);


        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.status;
            })
            .then(result => {
                console.log(result)
                console.log('Success: the store item was updated');
                window.location.replace(`/store/details/${storecode}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();