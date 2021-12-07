const addToCart = async (id, message) => {
	alert(message);
    return fetch(`https://localhost:5001/cart/add?id=${id}`,
        {
            method: "post"
        });
};