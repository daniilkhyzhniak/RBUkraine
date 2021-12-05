const decreaseAmount = async (id) => {
    return fetch(`https://localhost:5001/cart/${id}/decrease-amount`,
        {
            method: "post"
        }).then(total => Document.getElementById(total));
};