"use strict";
(function _storeItemRemove() {
    const formRemove =
        document.querySelector("#formRemove");
    formRemoveStoreItem.addEventListener('submit', e => {
        /// <summary>
        /// s the specified e.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        e.preventDefault();
        const url = "/api/storeitemapi/remove";
        const method = "post";
        const formData = new FormData(formRemove);
        console.log(`${url} ${method}`);
        const storecode = formData.get("StoreCode");

        fetch(url, {
            method: method,
            body: formData
        })
            .then(response => {
                /// <summary>
                /// s the specified response.
                /// </summary>
                /// <param name="response">The response.</param>
                /// <returns></returns>
                if (!response.ok) {
                    throw new Error('There was a network error!');
                }
                return response.status;
            })
            .then(result => {
                /// <summary>
                /// s the specified result.
                /// </summary>
                /// <param name="result">The result.</param>
                /// <returns></returns>
                console.log(result);
                console.log('Success: the store item was removed');
                window.location.replace(`/store/details/${storecode}`); // redirect
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });

})();