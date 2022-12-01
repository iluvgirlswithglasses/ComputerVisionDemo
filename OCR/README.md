
*this is my "Introduction to Information and Communication Technology" report*

# 1. OCR là gì

> Nhận dạng ký tự quang học *(tiếng Anh: Optical Character Recognition, viết tắt là OCR)*, là loại phần mềm máy tính được tạo ra để chuyển các hình ảnh của chữ viết tay hoặc chữ đánh máy (thường được quét bằng máy scanner) thành các văn bản tài liệu. OCR được hình thành từ một lĩnh vực nghiên cứu về nhận dạng mẫu, trí tuệ nhận tạo và machine vision. Mặc dù công việc nghiên cứu học thuật vẫn tiếp tục, một phần công việc của OCR đã chuyển sang ứng dụng trong thực tế với các kỹ thuật đã được chứng minh.

\- Nguồn: [wikipedia](https://vi.wikipedia.org/wiki/Nh%E1%BA%ADn_d%E1%BA%A1ng_k%C3%BD_t%E1%BB%B1_quang_h%E1%BB%8Dc)

# 2. Thuật toán để triển khai OCR

Tính đến nay, người ta đã nghiên cứu ra rất nhiều cách để để phát triển một chương trình OCR. Nhóm **Flaming Watermelon** chúng em đã nghiên cứu được một cách được nhiều phần mềm OCR tin dùng. Trong bài viết này, chúng em sẽ trình bày đại khái ý tưởng của cách làm ấy.

## 2.0. Convolution

Trước khi nói về thuật toán, ta đến với một phép toán sẽ được đề cập rất nhiều trong bài viết này - **Convolution**

#### 2.0.0. Hình dung

Convolution là phép toán thường được dùng trên *2 mảng k-chiều*, được ký hiệu bởi dấu $\ast$. Nói đại khái, gọi:

- $M_{x}$ là một điểm trong *không gian k-chiều* của mảng $M$
- $M_{f(n, x)}$ là các điểm lân cận $M_{x}$, có [Khoảng Cách Manhattan](https://vi.wikipedia.org/wiki/Kho%E1%BA%A3ng_c%C3%A1ch_Manhattan) với $M_{x}$ bé hơn hoặc bằng $n$ đơn vị.
- $F, G$ và $H$ là *3 mảng k-chiều*, trong đó, kích thước của $|G| = n^k$

Ta có giá trị của $H_{x}$ trong phép toán $H = F \ast G$ bằng:

$$\sum(F_{f(n, x)} \times G_{f(n, x)})$$

#### 2.0.1. Video ví dụ

Lấy ví dụ, phép toán $a \ast b$ giữa $a = \{1, 2, 3\}$ và $b = \{6, 5, 4\}$ được tính như sau:

[o1.webm](https://user-images.githubusercontent.com/58514512/205090479-5c47d1a2-d939-467f-a135-06b2c87e41bd.webm)

\- Nguồn: [3Blue1Brown](https://www.youtube.com/watch?v=KuXjwB4LzSA)

Ta áp dụng tương tự với 2 mảng có độ dài lớn hơn:

[o.webm](https://user-images.githubusercontent.com/58514512/205090748-dd941980-2479-4ce4-8fd1-de183633d7b4.webm)

\- Nguồn: [3Blue1Brown](https://www.youtube.com/watch?v=KuXjwB4LzSA)

#### 2.0.2. Khái quát hóa

2 ví dụ trên là minh họa cho phép toán $H = F \ast G$, với $H, F, G$ là *3 mảng 1 chiều*. Giả sử ta gọi:

$$k = \lfloor |H| \div 2 \rfloor$$ 

và cắt lấy phần giữa của mảng $H$ ra thành mảng $I$ sao cho: 

$$I = H\[ k : |H| - k \]$$

ta có:

$$I_{x} = \sum_{i=x-k}^{i=x+k}(F_{i} * G_{i})$$
