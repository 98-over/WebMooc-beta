<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Manager.aspx.cs" Inherits="Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="col-md-2 column" style="margin-top: 15px">
        <div class="panel-group" id="panel-733392">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <a class="panel-title" data-toggle="collapse" data-parent="#panel-733392" href="#panel-element-615605">角色管理</a>
                </div>
                <div id="panel-element-615605" class="panel-collapse in">
                    <div class="panel-body">
                        <asp:DataList runat="server" ID="RoleDL" OnItemCommand="RoleDL_ItemCommand">
                            <ItemTemplate>
                                <asp:Button runat="server" Text='<%# Eval("Name") %>' CommandName="showRoleDetail" CommandArgument='<%# Eval("Name") %>'
                                    Style="background-color: azure; border: none; width: 90px; text-align: left; margin-left: 10px; margin-bottom: 3px; font-size: 18px"></asp:Button>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <a class="panel-title" data-toggle="collapse" data-parent="#panel-733392" href="#panel-element-950913">老师管理</a>
                </div>
                <div id="panel-element-950913" class="panel-collapse collapse">
                    <div class="panel-body">
                        <asp:Label runat="server" ID="HaveTeaUser" Text="目前还没有老师用户!!!" Visible="false"></asp:Label>
                        <asp:DataList runat="server" ID="TeacherDL" OnItemCommand="TeacherDL_ItemCommand">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="tea_id" Value='<%# Eval("tea_id") %>' />
                                <asp:Button runat="server" Text='<%# Eval("tea_name") %>' CommandName="showTeaDetail" CommandArgument='<%# Eval("tea_name") %>'
                                    Style="background-color: azure; border: none; width: 90px; text-align: left; margin-left: 10px; margin-bottom: 3px; font-size: 18px"></asp:Button>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <a class="panel-title" data-toggle="collapse" data-parent="#panel-733392" href="#panel-element-950912">学生管理</a>
                </div>
                <div id="panel-element-950912" class="panel-collapse collapse">
                    <div class="panel-body">
                        <asp:Label runat="server" ID="HaveStuUser" Text="目前还没有学生用户!!!" Visible="false"></asp:Label>
                        <asp:DataList runat="server" ID="StudentDL" OnItemCommand="StudentDL_ItemCommand">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="stu_id" Value='<%# Eval("stu_id") %>' />
                                <asp:Button runat="server" Text='<%# Eval("stu_name") %>' CommandName="showStuDetail" CommandArgument='<%# Eval("stu_name") %>'
                                    Style="background-color: azure; border: none; width: 90px; text-align: left; margin-left: 10px; margin-bottom: 3px; font-size: 18px"></asp:Button>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <asp:Button runat="server" Text="创建角色" CssClass="btn btn-warning" OnClick="ShowCreateRole_Click"></asp:Button>
    </div>
    <div class="col-md-7 column" style="margin-top: 15px">
        <div id="showRole">
            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                <div style="width: 75%">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td style="width: 60px">角色名：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="lb_roleName" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">角色权限：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="readyRoleFun" Text=""></asp:Label><br />
                                    <br />
                                    <div runat="server" id="ShowNewRoleFun" visible="false" style="margin-top: 15px;">
                                        新增权限：<label style="color: red; font-size: 10px">(*请从右侧权限列表选择要添加的权限...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_NewRoleFun" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server" id="ShowRemoveRoleFun" visible="false" style="margin-top: 15px;">
                                        禁用已有权限：<label style="color: red; font-size: 10px">(*请从右侧权限列表选择要禁用的权限...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_RvRoleFun" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="text-align: center">
                        <asp:Button runat="server" Text="编辑权限" CssClass="btn btn-warning" OnClick="EditRoleFun_Click" ID="btn_EditRoleFun" />
                        <asp:Button runat="server" Text="保存" CssClass="btn btn-warning" OnClick="Submit_Click" ID="Submit" Visible="false" />
                        <asp:Button runat="server" Text="取消" CssClass="btn btn-warning" OnClick="Cancel_Click" ID="Cancel" Visible="false" />
                        <asp:Button runat="server" Text="删除角色" CssClass="btn btn-warning" OnClick="DeleteRole_Click" ID="btn_DeleteRoleFun" />
                    </div>
                </div>
                <div style="overflow-y: auto; height: 250px; width: 25%" runat="server" id="showEditRoleFun" visible="false">
                    <h5>权限列表</h5>
                    <asp:DataList runat="server" OnItemCommand="DL_editRoleFun_ItemCommand" ID="DL_editRoleFun">
                        <ItemTemplate>
                            <asp:Button runat="server" CssClass="list-group-item" ID="roleAddFun_select" Text='<%# Eval("fun_name") %>' CommandName="Add_FunRole" CommandArgument='<%# Eval("fun_name") %>' BorderStyle="None" Style="margin-bottom: 2px" />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>

        </div>
        <div id="showTeacher">
            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                <div style="width: 75%">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td style="width: 60px">用户名：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="lb_TeaName" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">用户角色：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="readyTeaRole" Text=""></asp:Label><br />
                                    <br />
                                    <div runat="server" id="ShowNewTeaRole" visible="false" style="margin-top: 15px;">
                                        新增角色：<label style="color: red; font-size: 10px">(*请从右侧角色列表选择要添加的角色...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_NewTeaRole" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server" id="ShowRemoveTeaRole" visible="false" style="margin-top: 15px;">
                                        移除已有角色：<label style="color: red; font-size: 10px">(*请从右侧角色列表选择要移除的角色...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_RvTeaRole" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="text-align: center">
                        <asp:Button runat="server" Text="编辑用户角色" CssClass="btn btn-warning" OnClick="EditTeaRole_Click" ID="btn_EditTeaRole" />
                        <asp:Button runat="server" Text="保存" CssClass="btn btn-warning" OnClick="Tea_Submit_Click" ID="tea_Submit" Visible="false" />
                        <asp:Button runat="server" Text="取消" CssClass="btn btn-warning" OnClick="Tea_Cancel_Click" ID="tea_Cancel" Visible="false" />
                    </div>
                </div>
                <div style="overflow-y: auto; height: 250px; width: 25%" runat="server" id="showTeaEditRole" visible="false">
                    <h5>角色列表</h5>
                    <asp:DataList runat="server" OnItemCommand="DL_EditTeaRole_ItemCommand" ID="DL_EditTeaRole">
                        <ItemTemplate>
                            <asp:Button runat="server" CssClass="list-group-item" ID="TeaAddRole_select" Text='<%# Eval("Name") %>' CommandName="Add_TeaRole" CommandArgument='<%# Eval("Name") %>' BorderStyle="None" Style="margin-bottom: 2px" />
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div id="showStudent">
            <div style="display: flex; flex-direction: row; justify-content: flex-start">
                <div style="width: 75%">
                    <table class="table table-bordered">
                        <tbody>
                            <tr>
                                <td style="width: 60px">用户名：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="lb_StuName" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50px">用户角色：
                                </td>
                                <td style="width: 250px">
                                    <asp:Label runat="server" ID="readyStuRole" Text=""></asp:Label><br />
                                    <br />
                                    <div runat="server" id="ShowNewStuRole" visible="false" style="margin-top: 15px;">
                                        新增角色：<label style="color: red; font-size: 10px">(*请从右侧角色列表选择要添加的角色...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_NewStuRole" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div runat="server" id="ShowRemoveStuRole" visible="false" style="margin-top: 15px;">
                                        移除已有角色：<label style="color: red; font-size: 10px">(*请从右侧角色列表选择要移除的角色...)</label><br />
                                        <div style="border: solid; border-color: lightgray; height: 80px; border-radius: 10px;">
                                            <asp:Label runat="server" ID="lb_RvStuRole" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="text-align: center">
                        <asp:Button runat="server" Text="编辑用户角色" CssClass="btn btn-warning" OnClick="EditStuRole_Click" ID="btn_EditStuRole" />
                        <asp:Button runat="server" Text="保存" CssClass="btn btn-warning" OnClick="Stu_Submit_Click" ID="stu_Submit" Visible="false" />
                        <asp:Button runat="server" Text="取消" CssClass="btn btn-warning" OnClick="Stu_Cancel_Click" ID="stu_Cancel" Visible="false" />
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div class="col-md-1 column" style="overflow-y: auto; height: 250px; width: 25%" runat="server" id="showStuEditRole" visible="false">
        <h5>角色列表</h5>
        <asp:DataList runat="server" OnItemCommand="DL_EditStuRole_ItemCommand" ID="DL_EditStuRole">
            <ItemTemplate>
                <asp:Button runat="server" CssClass="list-group-item" ID="StuAddRole_select" Text='<%# Eval("Name") %>' CommandName="Add_StuRole" CommandArgument='<%# Eval("Name") %>' BorderStyle="None" Style="margin-bottom: 2px" />
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="col-md-3 column" style="border: solid; border-radius: 10px; border-color: lightgray; margin-top: 15px" runat="server" id="ShowCreateRole" visible="false">
        <h4 style="text-align: center">创建角色</h4>
        <asp:Table runat="server">
            <asp:TableRow>
                <asp:TableCell>角色名：</asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="createRoleName" CssClass="form-control"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div style="display: flex; flex-direction: row; justify-content: flex-start; margin-top: 10px; height: 350px">
            <div style="width: 90%" id="newRoleFun">
                <asp:Label runat="server" ID="no_fun" Style="color: darkgray">请从右侧权限列表选择角色的权限...</asp:Label>
                <asp:Label runat="server" ID="role_fun" Text=""></asp:Label>
            </div>
            <div style="width: 55%; overflow-y: auto">
                权限列表<br />
                <asp:CheckBox runat="server" OnCheckedChanged="SelectAllFun" ID="CreateSelectAllFun" AutoPostBack="true" />全选
                <asp:DataList runat="server" ID="dl_fun" OnItemCommand="dl_fun_ItemCommand">
                    <ItemTemplate>
                        <asp:Button runat="server" CssClass="list-group-item" ID="fun_select" Text='<%# Eval("fun_name") %>' CommandName="add_role" CommandArgument='<%# Eval("fun_name") %>' BorderStyle="None" Style="margin-bottom: 2px" />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div style="text-align: center; margin-bottom: 10px">
            <asp:Button runat="server" Text="创建" CssClass="btn btn-default" OnClick="CreateRole_Click" />
            <asp:Button runat="server" Text="取消" CssClass="btn btn-warning" OnClick="HiddenCreateRole_Click" />
        </div>
    </div>
</asp:Content>

