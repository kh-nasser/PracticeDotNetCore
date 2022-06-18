function onReceiveMessageResult(text) {
	console.log(text);
}

function sendMessage(event) {
	event.preventDefault();//prevent refresh
	var text = $("#messageText").val();
	connection.invoke("SendMessage", text)
}

function insertGroup(event) {
	event.preventDefault();
	//console.log(event.target);
	var groupName = event.target[0].value;
	var imageFile = event.target[1].files[0];

	var formData = new FormData();
	formData.append("GroupName", groupName);
	formData.append("ImageFile", imageFile);
	$.ajax({
		url: "/home/CreateGroup",
		type: "post",
		data: formData,
		enctype: "multipart/form-data",
		processData: false,
		contentType: false
	});
	//var text = $("#groupName").val();
	//if(text){
	//	connection.invoke("CreateGroup", text);
	//}
}

function appendGroup(groupName, token, imageName) {
	console.log('appendGroup');
	//appendGroup
	if (groupName === "Error") {
		alert("ERROR!");
	} else {
		$(".rooms #userGroups ul").append(
			`<li onclick="JoinInGroup('${token}')">
				${groupName}
				<img src="/image/groups/${imageName}"/>
				<span>**</span> </li>`);

		$("#exampleModal").modal({show:false});
	}
}

function search() {
	var text = $("#searchInput").val();
	if (text) {
		$('#searchResult').show();
		$('#userGroups').hide();
		$.ajax({
			url="/home/search?title=" + text,
			type = "get",
		}).done(function (data) {
			for (var i in data) {
				$("#searchResult ul").html("");

				if (data[i].isUser) {
					$("#searchResult ul").append(
						`<li data-user-id='${data[i].token}'>
							${data[i].title}
							<img src="/img/${data[i].imageName}"/>
							<span></span>
						</li>`);
				}
				else {
					$("#searchResult ul").append(
						`<li onclick="JoinInGroup('${data[i].token}')">
							${data[i].title}
							<img src="/image/groups/${data[i].imageName}"/>
							<span>/*$createDate*/</span>
						</li>`);
				}
			}
		})
	} else {
		$('#searchResult').hide();
		$('#userGroups').show();
	}
}