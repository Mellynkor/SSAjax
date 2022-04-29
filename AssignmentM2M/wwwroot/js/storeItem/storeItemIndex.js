"use strict";
(function _storeItemIndex() { //Create function for the index
    const url = "/api/storeitemapi/storeitems"; //Create a constant value @ the URL
    fetch(url) //fetch the URL
        .then(response => { //Then take the response AND
            if (!response.ok) { //If the Response is NOT valid
                throw new Error('There was a network error!'); //Throw an error
            }
            return response.json(); //Otherwise return the response as Json
        })
        .then(result => { //Then take that Json result
            populateTable(result); //And populate the table with it
        })
        .catch(error => { //Catch any errors that may occur
            console.error('Error:', error); //And log them
        });
})();

function populateTable(result) { //Functional logic to populate the table with the Json result
    const tableBody = document.getElementById("tableBody"); //Create a constant variable called table body representing the table body
    result.forEach((item) => { //For each item in the result
        const tr = document.createElement("tr"); //Create a const TR element in the DOM
        for (let key in item) { //For each key in the item
            const td = document.createElement("td"); //Create a const TD element in the DOM
            let text = item[key]; //With the text being set to the item's key value
            if (text === '' && key === 'Name') { //If the text is empty  and the key = Name
                text = "No item"; //set the text to no item
            }
            let textNode = document.createTextNode(text); //Let each node of text be set to the text value
            td.appendChild(textNode); //Apprend the tds using the text node values
            tr.appendChild(td); //Append the trs using the tds
        }
        tableBody.appendChild(tr); //Append the tr to the table body
    });
}