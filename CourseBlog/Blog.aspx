<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script>
        $(document).ready(function () {
            var imgIndex = 1;
            var flist = [];
            $('#preimg').click(function () {
                var upstr = "#MainContent_uploadfile" + imgIndex.toString();
                $(upstr).click();

            });
            for (var i = 0; i < 9; i++) {
                $("#MainContent_uploadfile" + (i + 1).toString()).on("change", function (e) {
                    var file = e.target.files[0];
                    flist.push(new File([file], "newImg" + imgIndex.toString() + file.name.slice(file.name.indexOf(".")), { type: file.type }));
                    var fileTypes = ["bmp", "jpg", "png", "jpeg"];
                    var bTypeMatch = false;
                    for (var j = 0; j < fileTypes.length; j++) {
                        var start = file.name.lastIndexOf(".");
                        var fileType = file.name.substring(start + 1);
                        if (fileType.toLowerCase() == fileTypes[j]) {
                            bTypeMatch = true;
                            break;
                        }
                    }
                    if (bTypeMatch) {
                        var reader = new FileReader();
                        reader.readAsDataURL(file); // 读取文件
                        reader.onload = function (arg) {
                            var str = "#preimg" + imgIndex.toString();
                            var str1 = "#pimg" + imgIndex.toString();
                            imgIndex++;
                            if (imgIndex == 10) {
                                $("#addimg").hide();
                            }
                            $(str).show();
                            $(str1).attr("src", arg.target.result);
                            if (imgIndex != 10) {
                                if ((imgIndex - 1) % 3 == 0) {
                                    str = "#preimg" + imgIndex.toString();
                                    $(str).after($("#addimg"));
                                } else {
                                    $(str).after($("#addimg"));
                                }
                            }
                        }
                    }
                    else {
                        alert('仅限bmp，jpg，png，jpeg图片格式');
                    }

                });
            }
        });
        var index = 0;
        function showRemark(id) {
            $("#remark" + id).show();
        }
    </script>
    <style>
        .u_blog {
            display: flex;
            flex-direction: row;
            justify-content: flex-start;
        }

        .b_info {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .b_op {
            display: flex;
            flex-direction: row;
            justify-content: space-around;
            margin-top: 5px;
            margin-bottom: 5px
        }
    </style>

    <div class="container" style="padding-top: 15px">
        <div class="row clearfix">
            <div class="col-md-3 column">
                <div class="media-body">
                    <h3 class="media-heading">热门课程
                    </h3>
                    <ul>
                        <li><a href="#">C++从入门到精通</a>
                        </li>
                        <li><a href="#">JAVA面向对象编程</a>
                        </li>
                        <li><a href="#">ASP.NET WEB程序设计</a>
                        </li>
                        <li><a href="#">数据库原理</a>
                        </li>
                        <li><a href="#">计算机网络基础</a>
                        </li>
                        <li><a href="#">操作系统原理</a>
                        </li>
                        <li><a href="#">数据结构</a>
                        </li>
                        <li><a href="#">算法设计与分析</a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-5 column">
                <asp:DataList runat="server" ID="dl1" OnItemCommand="dl1_ItemCommand">
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="bno" Text='<%# Eval("b_no") %>' Visible="false"></asp:TextBox>
                        <asp:TextBox runat="server" ID="uid" Text='<%# Eval("u_id") %>' Visible="false"></asp:TextBox>
                        <div>
                            <div class="u_blog">
                                <div>
                                    <image src="/image/1.jpg" style="width: 45px; border-radius: 22px"></image>
                                </div>
                                <div class="b_info">
                                    <div style="color: orangered; font-size: 20px;display:flex;flex-direction:row;justify-content:flex-start">
                                        <div style="display:flex;flex-direction:column;justify-content:center"><%# Eval("stu_name") %></div>
                                        <div style="display:flex;flex-direction:column;justify-content:center">
                                            <asp:HiddenField runat="server" ID="u_name" Value='<%# Eval("stu_name") %>' />
                                            <asp:ImageButton runat="server" ID="btn_friend" ImageUrl="~/image/addfriend.png" Width="20" CommandName="AddFriend" CommandArgument='<%# Eval("u_id") %>'/>
                                        </div>
                                    </div>
                                    <div style="color: gray; font-size: 12px"><%# Eval("b_time") %>发布</div>
                                </div>
                            </div>
                            
                            <p style="margin-top: 10px"><%# Eval("b_text") %></p>
                            <div style="display: flex; flex-direction: column; justify-content: flex-start">
                                <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                    <div>
                                        <asp:Image ID="img1" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img2" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img3" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                </div>
                                <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                    <div>
                                        <asp:Image ID="img4" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img5" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img6" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                </div>
                                <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                    <div>
                                        <asp:Image ID="img7" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img8" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                    <div>
                                        <asp:Image ID="img9" runat="server" Visible="false" Style="object-fit: cover" />
                                    </div>
                                </div>
                            </div>
                            <div>相关课程：<a href="#"><%# Eval("course") %></a></div>
                            <div class="b_op">
                                <div></div>
                                <div onclick="showRemark('<%# Eval("b_no") %>')" style="display:flex;flex-direction:row;justify-content:flex-start">
                                    <div style="display:flex;flex-direction:column;justify-content:center">
                                        <asp:ImageButton ID="btnremark" runat="server" ImageUrl="/image/remark.png" style="width: 18px" CommandName="ShowRemark"></asp:ImageButton>
                                    </div>
                                    <div style="display:flex;flex-direction:column;justify-content:center">
                                        评论
                                    </div>
                                </div>
                                <div onclick="showRemark('<%# Eval("b_no") %>')" style="display:flex;flex-direction:row;justify-content:flex-start">
                                    <div style="display:flex;flex-direction:column;justify-content:center">
                                        <asp:ImageButton ID="zan" runat="server" ImageUrl="/image/zans.png" style="width: 17px" CommandName="Favour" CommandArgument='<%# Eval("b_no") %>'></asp:ImageButton>
                                    </div>
                                    <div>
                                        点赞(<asp:Label runat="server" ID="f_count" Text='<%# Eval("b_favour") %>'></asp:Label>)
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="remark<%# Eval("b_no")%>" style="display:none">
                            <asp:TextBox runat="server" ID="remark" Wrap="true" placeholder="评论一下..."></asp:TextBox>
                            <asp:Button runat="server" ID="btn_remark" Text="评论" CommandName="InsertRemark" CommandArgument='<%# Eval("b_no") %>'/>
                        </div>
                        <asp:DataList runat="server" ID="dl2">
                                <ItemTemplate>
                                    <div>
                                        <span style="color:dimgray"><%# Eval("stu_name") %>：</span>
                                        <p><%# Eval("r_text") %></p>
                                        <span><%# Eval("r_time") %></span>
                                        <asp:Button runat="server" Text="回复" CssClass="btn warning" Font-Size="8px"/>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-md-4 column" style="border: solid; border-radius: 10px; border-color: lightgray">
                <asp:TextBox runat="server" ID="b_text" TextMode="MultiLine" placeholder="分享一下今天的收获吧..." BorderStyle="None" Style="outline: none; overflow: hidden" Width="100%" Height="150px"></asp:TextBox><br />
                相关课程：
                <asp:DropDownList runat="server" ID="course_select" OnSelectedIndexChanged="course_select_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="--请选择--" Selected="True">--请选择--</asp:ListItem>
                    <asp:ListItem Value="C++">C++</asp:ListItem>
                    <asp:ListItem Value="Java">Java</asp:ListItem>
                    <asp:ListItem Value="Python">Python</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox runat="server" ID="course" CssClass="form-control"></asp:TextBox>
                <div style="display: flex; flex-direction: row; justify-content: flex-start">
                    <div style="display: flex; flex-direction: column; justify-content: flex-start">
                        <div style="display: flex; flex-direction: row; justify-content: space-around">
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg1" style="display: none">
                                    <img id="pimg1" src="#" width="70" height="70" style="object-fit: cover" />
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg2" style="display: none">
                                    <img id="pimg2" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg3" style="display: none">
                                    <img id="pimg3" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                        </div>
                        <div style="display: flex; flex-direction: row; justify-content: flex-start">
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg4" style="display: none">
                                    <img id="pimg4" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg5" style="display: none">
                                    <img id="pimg5" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg6" style="display: none">
                                    <img id="pimg6" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                        </div>
                        <div style="display: flex; flex-direction: row; justify-content: flex-start">
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg7" style="display: none">
                                    <img id="pimg7" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg8" style="display: none">
                                    <img id="pimg8" src="#" width="70" height="70" style="object-fit: cover;" />

                                </div>
                            </div>
                            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                                <div id="preimg9" style="display: none">
                                    <img id="pimg9" src="#" width="70" height="70" style="object-fit: cover;" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="addimg">
                        <img id="preimg" src="/image/addimg.png" width="70" />
                        <asp:FileUpload runat="server" ID="uploadfile1" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile2" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile3" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile4" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile5" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile6" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile7" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile8" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <asp:FileUpload runat="server" ID="uploadfile9" Style="display: none" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />
                        <%--<input id="uploadfile" type="file" style="visibility: hidden" accept=".bmp,.jpg,.png,.jpeg,image/bmp,image/jpg,image/png,image/jpeg" />--%>
                    </div>
                </div>
                <div style="display: flex; flex-direction: row; justify-content: center; margin-bottom: 10px; margin-top: 10px">
                    <asp:Button runat="server" CssClass="btn btn-warning" Text="发布" OnClick="InsertBlog_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

