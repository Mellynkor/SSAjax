"use strict";
(function _storeItemCreate() {
    const formCreateStoreItem =
        document.querySelector("#formCreateStoreItem");
    formCreateStoreItem.addEventListener('submit', e => {
        e.preventDefault();
        const url = "/api/storeitemapi/create";
        const method = "post";
        const formData = new FormData(formCreateStoreItem);
        console.log(`${url} ${method}`);

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.json();
            })
            .then(result => {
                console.log('Success: the store item was created');
                window.location.replace(`/store/details/${result.StoreCode}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();