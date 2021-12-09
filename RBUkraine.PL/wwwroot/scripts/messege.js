const alertMessage = async (message) => {
	alert(message);
};

const errorMessae = (massage, error) => {
	if (!error) {
		alert(massage);
	}
}