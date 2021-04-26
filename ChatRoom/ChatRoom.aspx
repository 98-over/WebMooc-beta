<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChatRoom.aspx.cs" Inherits="ChatRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="container" style="padding-top: 15px">
        <div class="row clearfix">
            <div class="col-md-3 column">
                <div class="media-body">
                    <h4>好友列表</h4>
                    <asp:HiddenField runat="server" ID="user_id" />
                    <asp:DataList runat="server" ID="f_dl" OnItemCommand="click_friend">
                        <ItemTemplate>
                            <asp:HiddenField runat="server" ID="f_name" Value='<%# Eval("nick_name") %>' />
                            <asp:HiddenField runat="server" ID="f_id" Value='<%# Eval("friend_id") %>' />
                            <asp:Button runat="server" Text='<%# Eval("nick_name") %>' CommandName="chat" CommandArgument='<%# Eval("friend_id") %>' />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="col-md-5 column" style="border: solid; border-radius: 10px; border-color: lightgray;">
                <div style="display:flex;flex-direction:row;justify-content:space-between">
                    <div>我的状态：<asp:Label runat="server" ID="my_net" Text=""></asp:Label></div>
                    <div>
                        <asp:Label runat="server" ID="fd_name" Text="请先选择好友"></asp:Label>
                        <asp:HiddenField runat="server" ID="recv_id" />
                    </div>
                    <div>好友状态：<asp:Label runat="server" ID="fd_net" Text=""></asp:Label></div>
                </div>
                <asp:TextBox runat="server" ID="hf_index" Text="0" Visible="false"></asp:TextBox>
                <div style="overflow-y: auto; height: 350px;margin-right:-15px;margin-left:-13px">
                    <asp:DataList runat="server" ID="meg_dl" Width="100%" style="">
                    <ItemTemplate>
                        <asp:HiddenField runat="server" ID="send_id" Value='<%# Eval("send_id") %>' />
                        <%
                            string h_index = this.hf_index.Text;
                            int index = int.Parse(h_index);
                            string my_id = User.Identity.GetUserId();
                            HiddenField hf = (HiddenField)meg_dl.Items[index].FindControl("send_id");
                            string send_id = hf.Value;
                            index++;
                            this.hf_index.Text = index.ToString();
                            if ( my_id == send_id)
                            {
                        %>
                            <div style="text-align:right;margin:2px"><%# Eval("meg") %></div>
                        <%
                            }
                            else
                            {
                        %>
                            <div style="text-align:left"><%# Eval("meg") %></div>
                        <%
                            }
                        %>
                    </ItemTemplate>
                </asp:DataList>
                </div>
                <div>
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="meg_box" placeholder="跟好友分享一下吧..." BorderStyle="None" Style="outline: none; overflow: hidden" Width="100%" Height="150px"></asp:TextBox>
                    <asp:Button runat="server" Text="发送" OnClick="Send_Click" OnClientClick="SendMessage()"/>
                </div>
            </div>
            <div class="col-md-4 column" style="border: solid; border-radius: 10px; border-color: lightgray">
            </div>
        </div>
    </div>
    <script>
        var ws = new WebSocket("ws://localhost:8183");
        ws.onopen = function () {
            console.log("open");
            var id = document.getElementById("MainContent_user_id").value;
            var data = {
                "op": "bind",
                "openId":id
            }
            ws.send(JSON.stringify(data));
        }
        ws.onmessage = function (e) {
            if (JSON.parse(e.data).type == "3") {
                alert("收到消息2");
                var data = JSON.parse(JSON.parse(e.data).data);
                var tr = document.createElement("tr");
                var td = document.createElement("td");
                var div = document.createElement("div");
                div.innerText = data.message;
                div.style.cssText = "text-align: left";
                td.appendChild(div);
                tr.appendChild(td);
                var table = document.getElementById("MainContent_meg_dl");
                table.appendChild(tr);
            } else if (JSON.parse(e.data).type == "2") {
                console.log("pong");
            }
        }
        function SendMessage() {
            alert("点击了发送");
            var receive_id = document.getElementById("MainContent_recv_id").value;
            var send_id = document.getElementById("MainContent_user_id").value;
            var message = document.getElementById("MainContent_meg_box").value;
            var meg_time = Date.now();
            var data = {
                "receive_id": receive_id,
                "send_id": send_id,
                "message": message,
                "meg_time": meg_time
            }
            ws.send(JSON.stringify(data));
        }
    </script>
</asp:Content>

