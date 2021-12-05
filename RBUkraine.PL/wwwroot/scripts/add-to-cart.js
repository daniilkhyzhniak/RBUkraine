const addToCart = async (id) => {
    return fetch(`https://localhost:5001/cart/add?id=${id}`,
        {
            method: "post"
        });
};