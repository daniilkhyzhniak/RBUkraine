const increaseAmount = async (id) => {
    return fetch(`https://localhost:5001/cart/${id}/increase-amount`,
        {
            method: "post"
        }).then(total => Document.getElementById(total));
};