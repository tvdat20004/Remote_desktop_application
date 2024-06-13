# Remote_desktop_application
Our final project in Network Programming at UIT
Nhóm 8: 
- Thái Vĩnh Đạt - 22520235
- Nguyễn Nhật Anh - 22520059
- Đinh Lê Thành Công - 22520167
## Sơ đồ kết nối:
![image](https://github.com/tvdat20004/Remote_desktop_application/assets/117071011/107b5382-a180-4c25-b4a4-705df7e26583)
- (1) Bước 1: 2 Client sẽ kết nối tới Server 1, sau đó sẽ gởi thông tin đăng kí, đăng nhập lên server 1 để kiểm tra tính hợp lệ. Server 1 sẽ trả về thông báo việc đăng nhập, đăng kí thành công hay thất bại.
- (2) Bước 2: Sau khi đăng nhập thành công thì Client sẽ kết nối lên Server 2 . Server 2 có nhiệm vụ lấy socket của client kết nối tới và gen ra một ID tương ứng, sau đó gởi ID về cho client tương ứng. Server sẽ lưu thông tin của các client và ID tương ứng.
- (3) Bước 3: Giả sử client A muốn điều khiển máy tính của client B, client A sẽ gởi request kết nối cho server 2 bao gồm ID của client B, kèm với một số đại diện cho port mà client A mở để lắng nghe kết nối.
- (4) Bước 4: Sau khi server 2 nhận được request kết nối của client A, nó sẽ trích xuất ID và port, sau đó tiến hành tìm kiếm dựa vào ID để lấy được socket của client B, sau đó sẽ lấy địa chỉ IP của A và port nhận từ A và gởi cho client B.
- (5) Bước 5: Sau khi client B có được IP và port của client A từ server 2, nó sẽ kết nối tới và bắt đầu một phiên làm việc giữa 2 client.
