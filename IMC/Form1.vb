Public Class Form1
    Dim peso, altura, resultado As Double


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Ingresar peso y altura")


        Else
            peso = TextBox1.Text
            altura = TextBox2.Text
            resultado = (peso / (altura * altura)) * 10000
            TextBox3.Text = resultado

            If (resultado > 18.5 And resultado < 24.899999999999999) Then
                lbln1.BackColor = Color.Yellow
                lbln2.BackColor = Color.Beige
                lbln3.BackColor = Color.Beige
                lbln4.BackColor = Color.Beige
                lbln5.BackColor = Color.Beige
            ElseIf resultado > 25 And resultado < 29.899999999999999 Then
                lbln1.BackColor = Color.Beige
                lbln2.BackColor = Color.Yellow
                lbln3.BackColor = Color.Beige
                lbln4.BackColor = Color.Beige
                lbln5.BackColor = Color.Beige
            ElseIf resultado > 30 And resultado < 34.899999999999999 Then
                lbln1.BackColor = Color.Beige
                lbln2.BackColor = Color.Beige
                lbln3.BackColor = Color.Yellow
                lbln4.BackColor = Color.Beige
                lbln5.BackColor = Color.Beige
            ElseIf resultado > 35 And resultado < 39.899999999999999 Then
                lbln1.BackColor = Color.Beige
                lbln2.BackColor = Color.Beige
                lbln3.BackColor = Color.Beige
                lbln4.BackColor = Color.Yellow
                lbln5.BackColor = Color.Beige
            ElseIf resultado > 40 Then
                lbln1.BackColor = Color.Beige
                lbln2.BackColor = Color.Beige
                lbln3.BackColor = Color.Beige
                lbln4.BackColor = Color.Beige
                lbln5.BackColor = Color.Yellow
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()

    End Sub
End Class
